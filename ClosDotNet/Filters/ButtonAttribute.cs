namespace ClosDotNet.Filters
{
    using System.Reflection;
    using System.Web.Mvc;

    public class ButtonAttribute : ActionMethodSelectorAttribute
    {
        public string ButtonName { get; set; }

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.Controller.ValueProvider.GetValue(ButtonName) != null;
        }
    }

}