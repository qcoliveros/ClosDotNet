using Common.Logging;

namespace ClosDotNet.Domain.Task
{
    using ClosDotNet.Domain.CallReport;
    using ClosDotNet.Domain.User;
    using System.Collections.Generic;

    public interface ITaskBO
    {
        IList<ICallReportVO> RetrieveCallReportTaskList(string loginId, bool isInitiator);

        int RetrieveCallReportTaskListCount(string loginId);
    }

    public class TaskBOImpl : ITaskBO
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TaskBOImpl));

        public IUserDAO UserDAO { private get; set; }
        public ITaskDAO TaskDAO { private get; set; }
        public ITaskADO TaskADO { private get; set; }

        public IList<ICallReportVO> RetrieveCallReportTaskList(string loginId, bool isInitiator)
        {
            IUserVO loginUser = UserDAO.RetrieveUserByLoginId(loginId);
            return TaskADO.RetrieveCallReportTaskList(loginUser.Id, isInitiator);
        }

        public int RetrieveCallReportTaskListCount(string loginId)
        {
            IUserVO loginUser = UserDAO.RetrieveUserByLoginId(loginId);
            return TaskADO.RetrieveCallReportTaskListCount(loginUser.Id);
        }
    }
}