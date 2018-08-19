namespace ClosDotNet.Domain.Task
{
    using ClosDotNet.Domain.Base.ORM;
    using ClosDotNet.Domain.CallReport;
    using ClosDotNet.Domain.CodeSet;
    using ClosDotNet.Domain.Customer;
    using ClosDotNet.Domain.User;
    using Spring.Data.Generic;
    using Spring.Stereotype;
    using Spring.Transaction.Interceptor;
    using System.Collections.Generic;
    using System.Data;

    public interface ITaskADO
    {
        
        IList<ICallReportVO> RetrieveCallReportTaskList(int loginId, bool isInitiator);

        int RetrieveCallReportTaskListCount(int loginId);
    }

    [Repository]
    public class TaskADOImpl : AdoDaoSupport, ITaskADO
    {
        private static readonly string RETRIEVE_CALL_REPORT_TASK_LIST = @" SELECT "
            + " sccr.customer_id, "
            + " (SELECT sc.customer_type FROM sml_customer sc WHERE id = sccr.customer_id) AS customer_type, "
            + " (SELECT sc.customer_name FROM sml_customer sc WHERE id = sccr.customer_id) AS customer_name, "
            + " sccr.id, "
            + " sccr.ref_no, "
            + " sccr.date_of_call, "
            + " (SELECT cv.code_value FROM code_value cv WHERE cv.id = sccr.purpose_of_call_id) AS purpose_desc, "            
            + " (SELECT su.full_name FROM sml_user su WHERE id = sccr.owner_id) AS owner_name, "
            + " (SELECT su.full_name FROM sml_user su WHERE id = st.current_owner_id) AS current_recipient, "
            + " st.workflow_definition_id AS process_definition "
            + " FROM sml_cust_call_report sccr INNER JOIN sml_task st ON sccr.id = st.reference_id "
            + " WHERE st.workflow_type = 'CALL_REPORT_TRX' "
            + " AND st.task_status = 'IN PROGRESS' ";

        private static readonly string RETRIEVE_CALL_REPORT_TASK_LIST_COUNT = @" SELECT COUNT (1) "
            + " FROM sml_cust_call_report sccr INNER JOIN sml_task st ON sccr.id = st.reference_id "
            + " WHERE st.workflow_type = 'CALL_REPORT_TRX' "
            + " AND st.task_status = 'IN PROGRESS' "
            + " AND ((st.initiator_id = @LoginId AND st.initiator_id = st.current_owner_id) "
            + " OR ( (st.initiator_id = @LoginId OR st.current_owner_id = @LoginId) "
            + " AND st.initiator_id <> st.current_owner_id)) ";

        [Transaction(ReadOnly = true)]
        public virtual IList<ICallReportVO> RetrieveCallReportTaskList(int loginId, bool isInitiator)
        {
            if (loginId != 0)
            {
                string query = RETRIEVE_CALL_REPORT_TASK_LIST;
                if (isInitiator)
                {
                    query += " AND st.initiator_id = @LoginId AND st.initiator_id = st.current_owner_id ";
                }
                else
                {
                    query += " AND (st.initiator_id = @LoginId OR st.current_owner_id = @LoginId) ";
                    query += " AND st.initiator_id <> st.current_owner_id ";
                }
                query += " ORDER BY sccr.date_of_call ";

                return (IList<ICallReportVO>)
                    AdoTemplate.QueryWithRowMapperDelegate(
                        CommandType.Text,
                        query,
                        delegate(IDataReader dataReader, int rowNum)
                        {
                            ICallReportVO callReport = new CallReportVO();
                            callReport.Id = dataReader.GetSafeInt32("id");
                            callReport.Customer = new CustomerVO() { 
                                Id = dataReader.GetSafeInt32("customer_id"),                                
                                CustomerType = dataReader.GetSafeString("customer_type"),
                                CustomerName = dataReader.GetSafeString("customer_name")
                            };
                            callReport.ReferenceNo = dataReader.GetSafeString("ref_no");
                            callReport.CallDate = dataReader.GetSafeDateTime("date_of_call");
                            callReport.CallPurpose = new CodeValueVO() { 
                                Description = dataReader.GetSafeString("purpose_desc") };                            
                            callReport.Owner = new UserVO() { FullName = dataReader.GetSafeString("owner_name") };
                            callReport.CurrentRecipient = new UserVO() { 
                                FullName = dataReader.GetSafeString("current_recipient") };
                            callReport.ProcessDefinition = dataReader.GetSafeString("process_definition");

                            return callReport;
                        },
                        "LoginId",
                        DbType.Int32,
                        0,
                        loginId
                    );
            }

            return null;
        }

        [Transaction(ReadOnly = true)]
        public virtual int RetrieveCallReportTaskListCount(int loginId)
        {
            if (loginId != 0)
            {
                return (int)
                    AdoTemplate.ExecuteScalar(
                        CommandType.Text,
                        RETRIEVE_CALL_REPORT_TASK_LIST_COUNT,
                        "LoginId",
                        DbType.Int32,
                        0,
                        loginId
                    );
            }

            return 0;
        }
    }
}