namespace ClosDotNet.Domain.Workflow
{
    using ClosDotNet.Domain.CodeSet;
    using ClosDotNet.Domain.Task;
    using ClosDotNet.Domain.User;
    using ClosDotNet.Resources.Resources;
    using System;
    using System.Collections.Generic;

    public interface IWorkflow
    {
        IList<IActionTypeVO> RetrieveWorkflowActions(Guid? processId);

        ITaskVO ExecuteWorkflow(ITaskVO task, IUserVO nextOwner);
    }

    public abstract class WorkflowBase : IWorkflow
    {
        public abstract IList<IActionTypeVO> RetrieveWorkflowActions(Guid? processId);

        public abstract ITaskVO ExecuteWorkflow(ITaskVO task, IUserVO nextOwner);

        protected IList<IActionTypeVO> RetrieveDefaultlActions()
        {
            IList<IActionTypeVO> actionList = new List<IActionTypeVO>();
            actionList.Add(new ActionTypeVO() 
            {
                Code = "Save",
                Description = CommonResources.OptionSave
            });

            return actionList;
        }
    }
}