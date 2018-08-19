namespace ClosDotNet.Domain.CallReport
{
    using ClosDotNet.Domain.Base.Model;
    using ClosDotNet.Domain.CodeSet;
    using ClosDotNet.Domain.Customer;
    using ClosDotNet.Domain.User;
    using System;

    public interface ICallReportVO : IDualControlVO
    {
        #region Actual VO Section
        ICustomerVO Customer { get; set; }

        string ReferenceNo { get; set; }

        DateTime? PreviousCallDate { get; set; }

        DateTime? CallDate { get; set; }

        DateTime? SiteVisitDate { get; set; }

        string ContactMode { get; set; }

        ICodeValueVO CallPurpose { get; set; }

        string Purpose { get; set; }

        string Attendees { get; set; }

        string FollowUp { get; set; }
        #endregion Actual VO Section

        #region Submit Section
        IUserVO Owner { get; set; }

        IUserVO Reviewer { get; set; }

        string Remarks { get; set; }
        #endregion Submit Section

        #region For View List
        string ProcessDefinition { get; set; }
        #endregion For View List

        #region For View Detail Logic
        IUserVO CurrentRecipient { get; set; }

        string TaskStatus { get; set; }

        Guid? WorkflowProcessId { get; set; }
        #endregion FFor View Detail Logic
    }

    [Serializable()]
    public class CallReportVO : DualControlVO, ICallReportVO
    {
        public static readonly string[] EXCLUDE_COPY = new string[] 
        {
            "Id",
            "VersionNumber",
            "CreateBy",
            "CreationDate",
            "Status",
            "Customer",
            "ReferenceNo",
            "Owner"
        };

        public CallReportVO() 
            : base ()
        {
        }

        public virtual ICustomerVO Customer { get; set; }

        public virtual string ReferenceNo { get; set; }

        public virtual DateTime? PreviousCallDate { get; set; }

        public virtual DateTime? CallDate { get; set; }

        public virtual DateTime? SiteVisitDate { get; set; }

        public virtual string ContactMode { get; set; }

        public virtual ICodeValueVO CallPurpose { get; set; }

        public virtual string Purpose { get; set; }

        public virtual string Attendees { get; set; }

        public virtual string FollowUp { get; set; }

        public virtual IUserVO Owner { get; set; }

        public virtual IUserVO Reviewer { get; set; }

        public virtual string Remarks { get; set; }

        public virtual string ProcessDefinition { get; set; }

        public virtual IUserVO CurrentRecipient { get; set; }

        public virtual string TaskStatus { get; set; }

        public virtual Guid? WorkflowProcessId { get; set; }
    }
}