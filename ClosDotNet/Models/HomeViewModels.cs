namespace ClosDotNet.Models
{
    public class TaskCountViewModel
    {
        public int OriginationDraftCount { get; set; }

        public int OriginationPendingApprovalCount { get; set; }

        public int OriginationPendingMyApprovalCount { get; set; }

        public int OriginationReturnedCount { get; set; }

        public int OriginationProcessedCount { get; set; }

        public int EnquiryReceivedCount { get; set; }

        public int EnquiryRepliedCount { get; set; }

        public int EnquiryPendingRecipientCount { get; set; }

        public int CallReportRecordCount { get; set; }

        public int TermSheetDraftCount { get; set; }

        public int TermSheetPendingMyApprovalCount { get; set; }
    }
}