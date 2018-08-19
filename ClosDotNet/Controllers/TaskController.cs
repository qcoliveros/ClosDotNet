namespace ClosDotNet.Controllers
{
    using ClosDotNet.Domain.CallReport;
    using ClosDotNet.Domain.Task;
    using ClosDotNet.Helper;
    using ClosDotNet.Mapper;
    using ClosDotNet.Models;
    using System.Collections.Generic;
    using System.Web.Mvc;

    [Authorize]
    public class TaskController : Controller
    {
        public ITaskBO TaskBO { get; set; }
        public IMapper TaskMapper { get; set; }

        public ActionResult ViewTaskCallReportList(TaskViewType viewType)
        {
            IList<CallReportTaskViewModel> modelList = new List<CallReportTaskViewModel>();
            IList<ICallReportVO> taskList = null;                
            if (viewType == TaskViewType.Draft)
            {
                taskList = TaskBO.RetrieveCallReportTaskList(User.Identity.Name, true);
                ViewBag.ActiveSubSubMenu = "TaskDraftCallReport";
            }
            else
            {
                taskList = TaskBO.RetrieveCallReportTaskList(User.Identity.Name, false);
                ViewBag.ActiveSubSubMenu = "TaskSubmittedCallReport";
            }

            if (taskList != null)
            {
                modelList = (IList<CallReportTaskViewModel>) TaskMapper.Map(
                    taskList, typeof(IList<ICallReportVO>), typeof(IList<CallReportTaskViewModel>));
            }

            return View(modelList);
        }
    }
}