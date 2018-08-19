using Common.Logging;

namespace ClosDotNet.Domain.CallReport
{
    using ClosDotNet.Domain.CodeSet;
    using ClosDotNet.Domain.Task;
    using ClosDotNet.Domain.User;
    using ClosDotNet.Domain.Workflow;
    using Spring.Transaction.Interceptor;
    using System;
    using System.Collections.Generic;

    public interface ICallReportManager
    {
        ICallReportVO RetrieveCallReport(int callReportId);

        IList<ICallReportVO> RetrieveCallReportsByCustomerId(int customerId);

        IList<IUserVO> RetrieveUserList(IList<string> excludedLoginIdList);

        IList<IActionTypeVO> RetrieveActionList(Guid? processId);

        ICallReportVO ProcessCallReport(ICallReportVO callReport, string action);
    }

    public class CallReportManager : ICallReportManager
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CallReportManager));
        
        public static readonly string WORKFLOW_TYPE = @"CALL_REPORT_TRX";
        
        public IUserDAO UserDAO { private get; set; }
        public ITaskDAO TaskDAO { private get; set; }
        public ICallReportDAO CallReportDAO { private get; set; }
        public ICallReportADO CallReportADO { private get; set; }
        public IWorkflow WorkflowManager { private get; set; }

        public ICallReportVO RetrieveCallReport(int callReportId)
        {
            return CallReportDAO.RetrieveCallReport(callReportId);
        }

        public IList<ICallReportVO> RetrieveCallReportsByCustomerId(int customerId)
        {
            return CallReportADO.RetrieveCallReportsByCustomerId(customerId);
        }

        public IList<IUserVO> RetrieveUserList(IList<string> excludedLoginIdList)
        {
            return UserDAO.RetrieveUserList(excludedLoginIdList);
        }

        public IList<IActionTypeVO> RetrieveActionList(Guid? processId)
        {
            return WorkflowManager.RetrieveWorkflowActions(processId);
        } 

        [Transaction]
        public ICallReportVO ProcessCallReport(ICallReportVO callReport, string action)
        {
            IUserVO loginUser = UserDAO.RetrieveUserByLoginId(callReport.LastUpdateBy);

            if (callReport.Id == 0)
            {
                callReport.Owner = loginUser;
            }
            CallReportDAO.SaveCallReport(callReport);

            ITaskVO task = TaskDAO.RetrieveTaskByRefIdAndFlowType(callReport.Id, WORKFLOW_TYPE);
            if (task == null)
            {
                task = new TaskVO() { ReferenceId = callReport.Id, WorkflowType = WORKFLOW_TYPE };
                task.LastUpdateBy = callReport.LastUpdateBy;
                task.Initiator = loginUser;
            }

            task.PreviousOwner = loginUser;
            task.TaskAction = action;

            task = WorkflowManager.ExecuteWorkflow(task, callReport.Reviewer);
            TaskDAO.SaveTask(task);

            callReport.Status = task.TaskStatus;
            callReport.WorkflowProcessId = task.WorkflowProcessId;
            callReport.TaskStatus = task.TaskStatus;
            callReport.CurrentRecipient = UserDAO.RetrieveUserById(task.CurrentOwner.Id);

            return callReport;
        }
    }
}