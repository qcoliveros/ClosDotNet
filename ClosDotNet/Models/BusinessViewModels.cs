namespace ClosDotNet.Models
{
    using Foolproof;
    using ClosDotNet.Resources.Resources;
    using System.ComponentModel.DataAnnotations;

    public class BusinessViewModel
    {
        public int Id { get; set; }

        [RequiredIfNotEmpty("CurrentPaidUpCapitalAmount")]
        [Display(Name = "LabelCurrentPaidUpCapitalCurrency", ResourceType = typeof(BusinessResources))]
        public string CurrentPaidUpCapitalCurrency { get; set; }

        [RequiredIfNotEmpty("CurrentPaidUpCapitalCurrency")]
        [RegularExpression(@"^\$?(\d{1,3},?(\d{3},?)*\d{3}(.\d{0,3})?|\d{1,3}(.\d{2})?)$",
            ErrorMessageResourceName = "MessageInvalidAmount",
            ErrorMessageResourceType=typeof(CommonResources))]
        [DataType(DataType.Currency)]
        [Display(Name = "LabelCurrentPaidUpCapitalAmount", ResourceType = typeof(BusinessResources))]
        public string CurrentPaidUpCapitalAmount { get; set; }

        [StringLength(3500)]
        [Display(Name = "LabelBorrowerBackground", ResourceType = typeof(BusinessResources))]
        public string BorrowerBackground { get; set; }

        [StringLength(3500)]
        [Display(Name = "LabelBusinessActivities", ResourceType = typeof(BusinessResources))]
        public string BusinessActivities { get; set; }

        [StringLength(3500)]
        [Display(Name = "LabelManagement", ResourceType = typeof(BusinessResources))]
        public string Management { get; set; }

        [StringLength(3500)]
        [Display(Name = "LabelAccountStrategy", ResourceType = typeof(BusinessResources))]
        public string AccountStrategy { get; set; }
    }
}