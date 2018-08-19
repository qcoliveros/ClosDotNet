using Common.Logging;

namespace ClosDotNet.Domain.Task
{
    using ClosDotNet.Domain.Base.ORM;
    using NHibernate.Criterion;
    using Spring.Stereotype;
    using Spring.Transaction.Interceptor;
    using System;

    public interface ITaskDAO
    {
        ITaskVO RetrieveTaskByRefIdAndFlowType(int refId, string workflowType);
     
        ITaskVO SaveTask(ITaskVO task);
    }

    [Repository]
    public class TaskDAOImpl : HibernateDAOBase, ITaskDAO
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TaskDAOImpl));

        [Transaction(ReadOnly = true)]
        public virtual ITaskVO RetrieveTaskByRefIdAndFlowType(int refId, string workflowType)
        {
            Logger.Debug("RetrieveTaskByRefIdAndFlowType|Reference ID: " + refId);
            Logger.Debug("RetrieveTaskByRefIdAndFlowType|Workflow Type: " + workflowType);

            return CurrentSession.CreateCriteria<ITaskVO>()
                .Add(Restrictions.Eq("ReferenceId", refId))
                .Add(Restrictions.Eq("WorkflowType", workflowType))
                .UniqueResult<ITaskVO>();
        }

        [Transaction]
        public virtual ITaskVO SaveTask(ITaskVO task)
        {
            if (task.Id == 0)
            {
                return CreateTask(task);
            }
            else
            {
                return UpdateTask(task);
            }
        }

        #region Helper Methods
        private ITaskVO CreateTask(ITaskVO task)
        {
            // LastUpdateBy will be set in the controller using the login user; hence, just copy.
            task.CreateBy = task.LastUpdateBy;
            task.CreationDate = DateTime.Now;
            task.LastUpdateDate = DateTime.Now;
            CurrentSession.SaveOrUpdate(task);

            return task;
        }
        
        private ITaskVO UpdateTask(ITaskVO task)
        {
            task.LastUpdateDate = DateTime.Now;
            CurrentSession.SaveOrUpdate(task);

            return task;
        }
        #endregion Helper Methods
    }
}