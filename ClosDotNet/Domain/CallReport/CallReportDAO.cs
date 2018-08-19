using Common.Logging;

namespace ClosDotNet.Domain.CallReport
{
    using ClosDotNet.Domain.Base.ORM;
    using Spring.Stereotype;
    using Spring.Transaction.Interceptor;
    using System;

    public interface ICallReportDAO
    {
        ICallReportVO RetrieveCallReport(int callReportId);

        ICallReportVO SaveCallReport(ICallReportVO callReport);
    }

    [Repository]
    public class CallReportDAOImpl : HibernateDAOBase, ICallReportDAO
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CallReportDAOImpl));

        private static readonly string RETRIEVE_NEXT_SEQUENCE =
            @" SELECT NEXT VALUE FOR sml_cust_call_report_seq ";

        [Transaction(ReadOnly = true)]
        public virtual ICallReportVO RetrieveCallReport(int callReportId)
        {
            return CurrentSession.Get<ICallReportVO>(callReportId);
        }

        [Transaction]
        public virtual ICallReportVO SaveCallReport(ICallReportVO callReport)
        {
            if (callReport.Id == 0)
            {
                return CreateCallReport(callReport);
            }
            else
            {
                return UpdateCallReport(callReport);
            }
        }

        #region Helper Methods
        private ICallReportVO CreateCallReport(ICallReportVO callReport)
        {
            // LastUpdateBy will be set in the controller using the login user; hence, just copy.
            callReport.CreateBy = callReport.LastUpdateBy;
            callReport.CreationDate = DateTime.Now;
            callReport.LastUpdateDate = DateTime.Now;
            callReport.ReferenceNo = "CR" + RetrieveNextSequence();
            CurrentSession.Save(callReport);

            return callReport;
        }

        private ICallReportVO UpdateCallReport(ICallReportVO callReport)
        {
            callReport.LastUpdateDate = DateTime.Now;
            CurrentSession.SaveOrUpdate(callReport);

            return callReport;
        }

        private int RetrieveNextSequence()
        {
            return Convert.ToInt32(
                CurrentSession.CreateSQLQuery(RETRIEVE_NEXT_SEQUENCE).UniqueResult<Int64>());
        }
        #endregion Helper Methods
    }
}