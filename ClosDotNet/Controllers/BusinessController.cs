namespace ClosDotNet.Controllers
{
    using ClosDotNet.Domain.Business;
    using ClosDotNet.Domain.Customer;
    using ClosDotNet.Helper;
    using ClosDotNet.Mapper;
    using ClosDotNet.Models;
    using ClosDotNet.Resources.Resources;
    using Common.Logging;
    using System;
    using System.Web.Mvc;

    [Authorize]
    public class BusinessController : CommonController
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BusinessController));

        public IBusinessBO BusinessBO { get; set; }
        public IMapper BusinessMapper { get; set; }

        public ActionResult ViewBusiness()
        {
            ClearSessionData();

            ICustomerVO sessionCustomer = (ICustomerVO)Session["SessionCustomer"];
            if (sessionCustomer != null)
            {
                IBusinessVO business = BusinessBO.RetrieveBusinessByCustomerId(sessionCustomer.Id);
                if (business == null)
                {
                    business = new BusinessVO();
                }
                Session["SessionBusiness"] = business;

                BusinessViewModel model = (BusinessViewModel) BusinessMapper.Map(
                    business, typeof(IBusinessVO), typeof(BusinessViewModel));
                if (string.IsNullOrEmpty(model.CurrentPaidUpCapitalCurrency))
                {
                    model.CurrentPaidUpCapitalCurrency = Constants.GetEnumDescription(Currency.SingaporeDollar);
                }

                // Needed to do this so that the client validation will not trigger.
                TempData["BusinessDetailModel"] = model;
            }

            return RedirectToAction("ViewBusinessDetails");
        }

        public ActionResult ViewBusinessDetails()
        {
            BusinessViewModel model = (BusinessViewModel) TempData["BusinessDetailModel"];

            ViewBag.CurrencyList = GetDropdownList(CodeSetCode.CURRENCY, BusinessMapper);
            ViewBag.DisplayMode = DisplayMode.Edit;
            if (User.IsInRole(Constants.GetEnumDescription(UserRole.CA)))
            {
                ViewBag.DisplayMode = DisplayMode.View;
            }
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveBusiness(BusinessViewModel model)
        {
            try
            {
                IBusinessVO business = (BusinessVO)
                    BusinessMapper.Map(model, typeof(BusinessViewModel), typeof(BusinessVO));
                business.LastUpdateBy = User.Identity.Name;

                ICustomerVO sessionCustomer = (ICustomerVO)Session["SessionCustomer"];
                business.Customer = sessionCustomer;

                if (business.Id == 0)
                {
                    business = BusinessBO.CreateBusiness(business);
                }
                else
                {
                    IBusinessVO sessionBusiness = (IBusinessVO)Session["SessionBusiness"];
                    business = BusinessBO.UpdateBusiness(sessionBusiness, business);
                }

                model = (BusinessViewModel)
                    BusinessMapper.Map(business, typeof(IBusinessVO), typeof(BusinessViewModel));

                Session["SessionBusiness"] = business;
                TempData["MessageType"] = MessageType.Success;
                TempData["MessageDescription"] = CommonResources.MessageSaveSuccess;
            }
            catch (Exception exception)
            {
                Logger.Debug("Exception encountered: " + exception.StackTrace);

                TempData["MessageType"] = MessageType.Error;
                TempData["MessageDescription"] = CommonResources.MessageSaveError + exception.Message;
            }

            TempData["BusinessDetailModel"] = model;
            return RedirectToAction("ViewBusinessDetails");
        }

        #region Utility Methods
        private void ClearSessionData()
        {
            Session["SessionBusiness"] = null;
        }
        #endregion Utility Methods
    }
}