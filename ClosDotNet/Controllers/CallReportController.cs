namespace ClosDotNet.Controllers
{
    using ClosDotNet.Domain.CallReport;
    using ClosDotNet.Domain.CodeSet;
    using ClosDotNet.Domain.Customer;
    using ClosDotNet.Domain.User;
    using ClosDotNet.Domain.Util;
    using ClosDotNet.Helper;
    using ClosDotNet.Mapper;
    using ClosDotNet.Models;
    using ClosDotNet.Resources.Resources;
    using Common.Logging;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    [Authorize]
    public class CallReportController : CommonController
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CustomerController));

        public ICustomerBO CustomerBO { get; set; }
        public ICallReportManager CallReportManager { get; set; }
        public IMapper CallReportMapper { get; set; }

        public ActionResult ViewCallReportList()
        {
            ClearSessionData();

            IEnumerable<ListCallReportViewModel> resultModel = new List<ListCallReportViewModel>();

            ICustomerVO sessionCustomer = (ICustomerVO) Session["SessionCustomer"];
            if (sessionCustomer != null)
            {
                IList<ICallReportVO> callReportList = CallReportManager.RetrieveCallReportsByCustomerId(sessionCustomer.Id);

                resultModel = (IEnumerable<ListCallReportViewModel>)
                    CallReportMapper.Map(callReportList, 
                    typeof(IList<ICallReportVO>), 
                    typeof(IEnumerable<ListCallReportViewModel>));
            }

            return View(resultModel);
        }

        public ActionResult ViewCallReport(int callReportId)
        {
            Logger.Debug("ViewCallReport|Selected Call Report ID: " + callReportId);

            CallReportViewModel model = null;
            if (callReportId != 0)
            {
                ICallReportVO callReport = CallReportManager.RetrieveCallReport(callReportId);
                if (callReport != null)
                {
                    model = (CallReportViewModel)
                        CallReportMapper.Map(callReport, typeof(ICallReportVO), typeof(CallReportViewModel));
                    Session["SessionCallReport"] = callReport;

                    if (Session["SessionCustomer"] == null)
                    {
                        if (Constants.GetEnumDescription(CustomerType.Individual).Equals(callReport.Customer.CustomerType))
                        {
                            Session["SessionCustomer"] =
                                (IIndividualCustomerVO)CustomerBO.RetrieveIndividualCustomer(callReport.Customer.Id);
                        }
                        else
                        {
                            Session["SessionCustomer"] =
                                (ICompanyCustomerVO)CustomerBO.RetrieveCompanyCustomer(callReport.Customer.Id);
                        }
                    }
                }

            }

            if (model == null)
            {
                ICustomerVO sessionCustomer = (ICustomerVO) Session["SessionCustomer"];

                model = new CallReportViewModel();
                model.CustomerName = sessionCustomer.CustomerName;
            }

            // Needed to do this so that the client validation will not trigger.
            TempData["CallReportDetailModel"] = model;
            return RedirectToAction("ViewCallReportDetails");
        }

        public ActionResult ViewCallReportDetails()
        {
            ICallReportVO sessionCallReport = (ICallReportVO) Session["SessionCallReport"];
            CallReportViewModel model = (CallReportViewModel) TempData["CallReportDetailModel"];

            ViewBag.CallPurposeList = GetDropdownList(CodeSetCode.CALL_PURPOSE, CallReportMapper);
            ViewBag.ReviewerList = GetUserList(new string[] { User.Identity.Name });
            ViewBag.ActionList = GetActionList(sessionCallReport != null ? sessionCallReport.WorkflowProcessId : null);
            ViewBag.DisplayMode = DisplayMode.Edit;

            if (sessionCallReport != null && !string.IsNullOrEmpty(sessionCallReport.TaskStatus)
                && ("COMPLETED".Equals(sessionCallReport.TaskStatus) 
                    || (sessionCallReport.CurrentRecipient != null 
                        && !User.Identity.Name.Equals(sessionCallReport.CurrentRecipient.LoginId))))
            {
                ViewBag.DisplayMode = DisplayMode.View;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessCallReport(CallReportViewModel model, ActionType actionType)
        {
            Logger.Debug("SaveCallReport|Action type: " + actionType);

            if (actionType == ActionType.Process)
            {
                try
                {
                    ICallReportVO callReport = (CallReportVO)
                        CallReportMapper.Map(model, typeof(CallReportViewModel), typeof(CallReportVO));

                    ICallReportVO sessionCallReport = (ICallReportVO) Session["SessionCallReport"];
                    if (callReport.Id == 0 || sessionCallReport == null)
                    {
                        sessionCallReport = new CallReportVO();
                    }

                    AccessorUtil.copyValue(callReport, sessionCallReport, CallReportVO.EXCLUDE_COPY);

                    sessionCallReport.LastUpdateBy = User.Identity.Name;

                    ICustomerVO sessionCustomer = (ICustomerVO) Session["SessionCustomer"];
                    sessionCallReport.Customer = sessionCustomer;

                    sessionCallReport = CallReportManager.ProcessCallReport(sessionCallReport, model.Action);

                    model = (CallReportViewModel)
                        CallReportMapper.Map(sessionCallReport, typeof(ICallReportVO), typeof(CallReportViewModel));

                    Session["SessionCallReport"] = sessionCallReport;
                    TempData["MessageType"] = MessageType.Success;
                    TempData["MessageDescription"] = CommonResources.MessageSaveSuccess;
                } 
                catch (Exception exception)
                {
                    Logger.Debug("Exception encountered: " + exception.StackTrace);

                    TempData["MessageType"] = MessageType.Error;
                    TempData["MessageDescription"] = CommonResources.MessageSaveError + exception.Message;
                }

                TempData["CallReportDetailModel"] = model;
                return RedirectToAction("ViewCallReportDetails");
            }

            return RedirectToAction("ViewCallReportList");
        }

        #region Utility Methods
        private IList<SelectListItem> GetActionList(Guid? processId)
        {
            IList<IActionTypeVO> actionList = CallReportManager.RetrieveActionList(processId);
            Logger.Debug("Search|Action DDL: " + (actionList != null ? actionList : null));

            return (IList<SelectListItem>)
                CallReportMapper.Map(actionList, typeof(IList<IActionTypeVO>), typeof(IList<SelectListItem>));
        }

        private IList<SelectListItem> GetUserList(IList<string> excludedUserLoginIdList)
        {
            IList<IUserVO> userList = CallReportManager.RetrieveUserList(excludedUserLoginIdList);
            Logger.Debug("Search|User DDL: " + (userList != null ? userList : null));

            return (IList<SelectListItem>)
                CallReportMapper.Map(userList, typeof(IList<IUserVO>), typeof(IList<SelectListItem>));
        }

        private void ClearSessionData()
        {
            Session["SessionCallReport"] = null;
        }
        #endregion Utility Methods
    }
}