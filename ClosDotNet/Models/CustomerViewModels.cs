namespace ClosDotNet.Models
{
    using Foolproof;
    using ClosDotNet.Helper;
    using ClosDotNet.Resources.Resources;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SeachCustomerNameViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        [Display(Name = "LabelSearchCustomerName", ResourceType = typeof(CustomerResources))]
        public string CustomerName { get; set; }
    }

    public class SeachCifNumberViewModel
    {
        [Required]
        [StringLength(19)]
        [Display(Name = "LabelSearchCifNumber", ResourceType = typeof(CustomerResources))]
        public string CifNumber { get; set; }
    }

    public class SeachIdCombinationViewModel
    {
        [Required]
        [Display(Name = "LabelSearchIdType", ResourceType = typeof(CustomerResources))]
        public int IdType { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 5)]
        [Display(Name = "LabelSearchIdNumber", ResourceType = typeof(CustomerResources))]
        public string IdNumber { get; set; }

        [Required]
        [Display(Name = "LabelSearchIdIssuedCountry", ResourceType = typeof(CustomerResources))]
        public string IdIssuedCountry { get; set; }
    }

    public class SearchViewModel
    {
        public SeachCustomerNameViewModel SearchCustomerName { get; set; }

        public SeachCifNumberViewModel SeachCifNumber { get; set; }

        public SeachIdCombinationViewModel SeachIdCombination { get; set; }
    }

    public class SearchResultViewModel
    {
        public string CustomerType { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CifNumber { get; set; }

        public string IdType { get; set; }

        public string IdNumber { get; set; }

        public string IdCountry { get; set; }

        public string RmName { get; set; }
    }

    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
            RelationshipManager = new RelationshipManager();
        }

        public int Id { get; set; }

        public string CustomerType { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        [Display(Name = "LabelDetailLegalName", ResourceType = typeof(CustomerResources))]
        public string CustomerName { get; set; }

        [StringLength(50, MinimumLength = 5)]
        [Display(Name = "LabelDetailPreviousLegalName", ResourceType = typeof(CustomerResources))]
        public string PreviousCustomerName { get; set; }

        [Editable(false)]
        [Display(Name = "LabelDetailCifNumber", ResourceType = typeof(CustomerResources))]
        public string CifNumber { get; set; }

        [Required]
        [Display(Name = "LabelDetailIdType", ResourceType = typeof(CustomerResources))]
        public string IdType { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        [Display(Name = "LabelDetailIdNumber", ResourceType = typeof(CustomerResources))]
        public string IdNumber { get; set; }

        [Required]
        [Display(Name = "LabelDetailIdIssuedCountry", ResourceType = typeof(CustomerResources))]
        public string IdIssuedCountry { get; set; }

        [Required]
        [Display(Name = "LabelDetailClassification", ResourceType = typeof(CustomerResources))]
        public string Classification { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "LabelDetailAccountOpenedDate", ResourceType = typeof(CustomerResources))]
        public string AccountOpenedDate { get; set; }

        [Range(1950, 2015)]
        [DisplayFormat(DataFormatString = "{0:yyyy}")]
        [Display(Name = "LabelDetailRelationshipSince", ResourceType = typeof(CustomerResources))]
        public string RelationshipSince { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "LabelDetailFacilityGrantedDate", ResourceType = typeof(CustomerResources))]
        public string FacilityGrantedDate { get; set; }
        
        [Display(Name = "LabelDetailAddressHoldMail", ResourceType = typeof(CustomerResources))]
        public string HoldMailInd { get; set; }

        public AddressViewModel Address { get; set; }

        public RelationshipManager RelationshipManager { get; set; }
    }

    public class RelationshipManager
    {
        public int Id { get; set; }

        [Display(Name = "LabelDetailPrimaryOfficerName", ResourceType = typeof(CustomerResources))]
        public string FullName { get; set; }
    }

    public class CompanyCustomerViewModel : CustomerViewModel
    {
        public CompanyCustomerViewModel() 
            : base()
        {
            Address = new AddressViewModel();
            Address.RegisteredAddress = new AddressDetailViewModel() 
                { AddressType = Constants.GetEnumDescription(AddressType.Business) };
            Address.MailingAddress = new AddressDetailViewModel() 
                { AddressType = Constants.GetEnumDescription(AddressType.Mailing) };
        }

        [Required]
        [Display(Name = "LabelDetailConstitution", ResourceType = typeof(CustomerResources))]
        public string Constitution { get; set; }

        [Display(Name = "LabelDetailMandatoryInfoNotAvailable", ResourceType = typeof(CustomerResources))]
        public bool MandatoryInfoNotAvailableInd { get; set; }

        [Required]
        [Display(Name = "LabelDetailIncorporationCountry", ResourceType = typeof(CustomerResources))]
        public string IncorporationCountry { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "LabelDetailIncorporationDate", ResourceType = typeof(CustomerResources))]
        public string IncorporationDate { get; set; }

        [Required]
        [Display(Name = "LabelDetailBusinessOperationCountry", ResourceType = typeof(CustomerResources))]
        public string BusinessOperationCountry { get; set; }

        [Display(Name = "LabelDetailMoreThan50S35", ResourceType = typeof(CustomerResources))]
        public string MoreThan50TurnoverInd { get; set; }
    }

    public class IndividualCustomerViewModel : CustomerViewModel
    {
        public IndividualCustomerViewModel()
            : base()
        {
            Address = new AddressViewModel();
            Address.RegisteredAddress = new AddressDetailViewModel() 
                { AddressType = Constants.GetEnumDescription(AddressType.Home) };
            Address.MailingAddress = new AddressDetailViewModel() 
                { AddressType = Constants.GetEnumDescription(AddressType.Mailing) };
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "LabelDetailWithBankSinceDate", ResourceType = typeof(CustomerResources))]
        public string WithBankSinceDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "LabelDetailFirstCreditGrantedDate", ResourceType = typeof(CustomerResources))]
        public string FirstCreditGrantedDate { get; set; }

        [Required]
        [Display(Name = "LabelDetailSalutation", ResourceType = typeof(CustomerResources))]
        public string Salutation { get; set; }

        [Display(Name = "LabelDetailPreviousSalutation", ResourceType = typeof(CustomerResources))]
        public string PreviousSalutation { get; set; }

        [Required]
        [Display(Name = "LabelDetailGender", ResourceType = typeof(CustomerResources))]
        public string Gender { get; set; }

        [Display(Name = "LabelDetailMaritalStatus", ResourceType = typeof(CustomerResources))]
        public string MaritalStatus { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "LabelDetailBirthDate", ResourceType = typeof(CustomerResources))]
        public string BirthDate { get; set; }

        [Editable(false)]
        [Display(Name = "LabelDetailAge", ResourceType = typeof(CustomerResources))]
        public string Age { get; set; }

        [Range(0, 100)]
        [Display(Name = "LabelDetailDependentCount", ResourceType = typeof(CustomerResources))]
        public string DependentCount { get; set; }

        [Required]
        [Display(Name = "LabelDetailNationality", ResourceType = typeof(CustomerResources))]
        public string Nationality { get; set; }

        [Required]
        [Display(Name = "LabelDetailResidentCountry", ResourceType = typeof(CustomerResources))]
        public string ResidentCountry { get; set; }

        [Required]
        [Display(Name = "LabelDetailPermanentResidentInd", ResourceType = typeof(CustomerResources))]
        public string PermanentResidentInd { get; set; }

        [Display(Name = "LabelDetailRace", ResourceType = typeof(CustomerResources))]
        public string Race { get; set; }

        [Display(Name = "LabelDetailHighestEducation", ResourceType = typeof(CustomerResources))]
        public string HighestEducation { get; set; }
    }

    public class AddressViewModel
    {
        public AddressDetailViewModel HomeAddress { get; set; }

        public AddressDetailViewModel RegisteredAddress { get; set; }

        public AddressDetailViewModel MailingAddress { get; set; }
    }

    public class AddressDetailViewModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string AddressType { get; set; }

        [Required]
        [Display(Name = "LabelDetailAddressFormat", ResourceType = typeof(CustomerResources))]
        public string AddressFormat { get; set; }
        
        [StringLength(7)]
        [Display(Name = "LabelDetailAddressBlockNo", ResourceType = typeof(CustomerResources))]
        public string StructuredBlock { get; set; }

        [StringLength(200)]
        [Display(Name = "LabelDetailAddressStreet", ResourceType = typeof(CustomerResources))]
        public string StructuredStreet { get; set; }

        [StringLength(4)]
        [Display(Name = "LabelDetailAddressStorey", ResourceType = typeof(CustomerResources))]
        public string StructuredStorey { get; set; }

        [StringLength(7)]
        [Display(Name = "LabelDetailAddressUnit", ResourceType = typeof(CustomerResources))]
        public string StructuredUnit { get; set; }

        [StringLength(40)]
        [Display(Name = "LabelDetailAddressBuildingName", ResourceType = typeof(CustomerResources))]
        public string StructuredBuilding { get; set; }

        [StringLength(30)]
        [Display(Name = "LabelDetailAddressCity", ResourceType = typeof(CustomerResources))]
        public string StructuredCity { get; set; }

        [StringLength(10)]
        [Display(Name = "LabelDetailAddressPostalCode", ResourceType = typeof(CustomerResources))]
        public string StructuredPostalCode { get; set; }

        [Required]
        [Display(Name = "LabelDetailAddressCountry", ResourceType = typeof(CustomerResources))]
        public string StructuredCountry { get; set; }

        [StringLength(6)]
        [Display(Name = "LabelDetailAddressPoBox", ResourceType = typeof(CustomerResources))]
        public string StructuredPoBox { get; set; }

        [StringLength(200)]
        [Display(Name = "LabelDetailUnstructuredAddress", ResourceType = typeof(CustomerResources))]
        public string UnstructuredAddress1 { get; set; }

        [StringLength(85)]
        public string UnstructuredAddress2 { get; set; }

        [StringLength(85)]
        public string UnstructuredAddress3 { get; set; }

        [StringLength(85)]
        public string UnstructuredAddress4 { get; set; }

        [Display(Name = "LabelDetailResidentialOwnership", ResourceType = typeof(CustomerResources))]
        public string ResidentialOwnershipType { get; set; }

        [Display(Name = "LabelDetailResidentialType", ResourceType = typeof(CustomerResources))]
        public string ResidentialType { get; set; }

        [Range(1950, 2015)]
        [Display(Name = "LabelDetailResidentialStayStartDate", ResourceType = typeof(CustomerResources))]
        public string ResidentialStayStartYear { get; set; }

        [Display(Name = "LabelDetailResidentialStayLength", ResourceType = typeof(CustomerResources))]
        public string ResidentialLengthOfStay { get; set; }
    }
}