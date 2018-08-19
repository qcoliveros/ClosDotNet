namespace ClosDotNet.Controllers
{
    using ClosDotNet.Domain.Task;
    using ClosDotNet.Models;
    using System.Web.Mvc;

    [Authorize]
    public class HomeController : Controller
    {
        public ITaskBO TaskBO { get; set; }

        public ActionResult Welcome()
        {
            Session.Clear();
            TempData.Clear();

            TaskCountViewModel model = new TaskCountViewModel();
            model.CallReportRecordCount = TaskBO.RetrieveCallReportTaskListCount(User.Identity.Name);

            return View(model);
        }
    }
}