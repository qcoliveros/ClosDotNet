namespace ClosDotNet.Models
{
    using ClosDotNet.Resources.Resources;
    using Foolproof;
    using System.ComponentModel.DataAnnotations;

    public class ListCallReportViewModel
    {
        public int Id { get; set; }

        public string ReferenceNo { get; set; }

        public string CallPurpose { get; set; }

        public string CallDate { get; set; }

        public string Originator { get; set; }

        public string LastUpdateDate { get; set; }

        public string CurrentRecipient { get; set; }

        public string Status { get; set; }

        public string Workflow { get; set; }
    }

    public class CallReportViewModel
    {
        public int Id { get; set; }

        [Display(Name = "LabelReportDetailCustomerName", ResourceType = typeof(CallReportResources))]
        public string CustomerName { get; set; }

        [Display(Name = "LabelReportDetailReferenceNo", ResourceType = typeof(CallReportResources))]
        public string ReferenceNo { get; set; }

        [Display(Name = "LabelReportDetailPreviousCallDate", ResourceType = typeof(CallReportResources))]
        public string PreviousCallDate { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "LabelReportDetailCallDate", ResourceType = typeof(CallReportResources))]
        public string CallDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "LabelReportDetailSiteVisitDate", ResourceType = typeof(CallReportResources))]
        public string SiteVisitDate { get; set; }

        [Required]
        [Display(Name = "LabelReportDetailContactMode", ResourceType = typeof(CallReportResources))]
        public char ContactMode { get; set; }

        [Required]
        [Display(Name = "LabelReportDetailCallPurpose", ResourceType = typeof(CallReportResources))]
        public int CallPurpose { get; set; }

        [StringLength(1000)]
        [Display(Name = "LabelReportDetailAdditionalPurpose", ResourceType = typeof(CallReportResources))]
        public string Purpose { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "LabelReportDetailAttendees", ResourceType = typeof(CallReportResources))]
        public string Attendees { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "LabelReportDetailFollowUp", ResourceType = typeof(CallReportResources))]
        public string FollowUp { get; set; }
        
        #region Submit Section
        [Required]
        [Display(Name = "LabelReportSubmitAction", ResourceType = typeof(CallReportResources))]
        public string Action { get; set; }

        [RequiredIf("Action", "Submit",
            ErrorMessageResourceName = "MessageRequiredField",
            ErrorMessageResourceType = typeof(CommonResources))]
        [Display(Name = "LabelReportSubmitReviewer", ResourceType = typeof(CallReportResources))]
        public string ReviewerId { get; set; }
        #endregion Submit Section
    }
}