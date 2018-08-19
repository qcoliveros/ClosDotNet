namespace ClosDotNet.Domain.CallReport
{
    using ClosDotNet.Domain.Base.ORM;
    using ClosDotNet.Domain.CodeSet;
    using ClosDotNet.Domain.Customer;
    using ClosDotNet.Domain.User;
    using Spring.Data.Generic;
    using Spring.Stereotype;
    using Spring.Transaction.Interceptor;
    using System.Collections.Generic;
    using System.Data;

    public interface ICallReportADO
    {
        IList<ICallReportVO> RetrieveCallReportsByCustomerId(int customerId);
    }

    [Repository]
    public class CallReportADOImpl : AdoDaoSupport, ICallReportADO
    {
        private static readonly string RETRIEVE_CALL_REPORT_BY_CUSTOMER = @" SELECT "
            + " sccr.id, "
            + " sccr.ref_no, "
            + " (SELECT cv.code_value FROM code_value cv WHERE cv.id = sccr.purpose_of_call_id) AS purpose_desc, "
            + " sccr.date_of_call, "
            + " (SELECT su.full_name FROM sml_user su WHERE id = sccr.owner_id) AS owner_name, "
            + " sccr.last_update_date, "
            + " (SELECT su.full_name FROM sml_user su WHERE id = st.current_owner_id) AS current_recipient, "
            + " st.status, "
            + " st.workflow_definition_id AS process_definition "
            + " FROM sml_cust_call_report sccr INNER JOIN sml_task st ON sccr.id = st.reference_id "
            + " WHERE sccr.customer_id = @CustomerId "
            + " AND st.workflow_type = 'CALL_REPORT_TRX' "
            + " ORDER BY sccr.date_of_call DESC ";

        [Transaction(ReadOnly = true)]
        public virtual IList<ICallReportVO> RetrieveCallReportsByCustomerId(int customerId)
        {
            if (customerId != 0)
            {
                return (IList<ICallReportVO>) 
                    AdoTemplate.QueryWithRowMapperDelegate(
                        CommandType.Text,
                        RETRIEVE_CALL_REPORT_BY_CUSTOMER,
                        delegate(IDataReader dataReader, int rowNum)
                        {
                            ICallReportVO callReport = new CallReportVO();
                            callReport.Id = dataReader.GetSafeInt32("id");
                            callReport.ReferenceNo = dataReader.GetSafeString("ref_no");
                            callReport.CallPurpose = new CodeValueVO() { 
                                Description = dataReader.GetSafeString("purpose_desc") };
                            callReport.CallDate = dataReader.GetSafeDateTime("date_of_call");
                            callReport.Owner = new UserVO() { FullName = dataReader.GetSafeString("owner_name") };
                            callReport.LastUpdateDate = dataReader.GetSafeDateTime("last_update_date");
                            callReport.CurrentRecipient = new UserVO() { 
                                FullName = dataReader.GetSafeString("current_recipient") };
                            callReport.Status = dataReader.GetSafeString("status");
                            callReport.ProcessDefinition = dataReader.GetSafeString("process_definition");

                            return callReport;
                        }, 
                        "CustomerId", 
                        DbType.Int32, 
                        0, 
                        customerId
                    );
            }

            return null;
        }
    }
}