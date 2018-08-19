namespace ClosDotNet.Models
{
    public class CallReportTaskViewModel
    {
        public string CustomerType { get; set; }

        public int CustomerId { get; set; }

        public int CallReportId { get; set; }

        public string CustomerName { get; set; }

        public string OriginatorName { get; set; }

        public string LastRecipientName { get; set; }

        public string ReferenceNo { get; set; }

        public string CallDate { get; set; }

        public string CallPurpose { get; set; }

        public string Workflow { get; set; }
    }
}