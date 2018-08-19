namespace ClosDotNet.Domain.Task
{
    using ClosDotNet.Domain.Base.Model;
    using ClosDotNet.Domain.User;
    using System;

    public interface ITaskVO : IValueObject
    {
        IUserVO Initiator { get; set; }

        IUserVO PreviousOwner { get; set; }

        IUserVO CurrentOwner { get; set; }

        int ReferenceId { get; set; }

        string Status { get; set; }

        #region Workflow Related
        Guid? WorkflowProcessId { get; set; }

        Guid? WorkflowDefinitionId { get; set; }

        string WorkflowType { get; set; }

        string TaskAction { get; set; }

        string TaskStatus { get; set; }
        #endregion Workflow Related
    }

    public class TaskVO : ValueObject, ITaskVO
    {
        public TaskVO()
            : base()
        {
        }

        public virtual IUserVO Initiator { get; set; }

        public virtual IUserVO PreviousOwner { get; set; }

        public virtual IUserVO CurrentOwner { get; set; }

        public virtual int ReferenceId { get; set; }

        public virtual string Status { get; set; }

        public virtual Guid? WorkflowProcessId { get; set; }

        public virtual Guid? WorkflowDefinitionId { get; set; }

        public virtual string WorkflowType { get; set; }

        public virtual string TaskAction { get; set; }

        public virtual string TaskStatus { get; set; }
    }
}