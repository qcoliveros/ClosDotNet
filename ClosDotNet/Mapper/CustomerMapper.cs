namespace ClosDotNet.Mapper
{
    using AutoMapper;
    using ClosDotNet.Domain.Customer;
    using ClosDotNet.Domain.User;
    using ClosDotNet.Filters;
    using ClosDotNet.Models;
    using ClosDotNet.Helper;
    using System;
    using System.Collections.Generic;

    public class CustomerMapper : MapperBase
    {
        public CustomerMapper() 
            : base()
        {
            // Search Result
            Mapper.CreateMap<ICustomerVO, SearchResultViewModel>()
                .ForMember(
                    dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.IdType, opt => opt.MapFrom(src => src.IdType.Description))
                .ForMember(
                    dest => dest.IdCountry, opt => opt.MapFrom(src => src.IdIssuedCountry.Description))
                .ForMember(
                    dest => dest.RmName, opt => opt.MapFrom(src => src.RelationshipManager.FullName));

            // Customer Details : VO to Form
            Mapper.CreateMap<IUserVO, RelationshipManager>();

            Mapper.CreateMap<IAddressVO, AddressDetailViewModel>()
                .ForMember(
                    dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
                .ForMember(
                    dest => dest.AddressFormat, opt => opt.MapFrom(src => src.AddressFormat.Id))
                .ForMember(
                    dest => dest.StructuredCountry, opt => opt.MapFrom(src => src.StructuredCountry.Code))
                .ForMember(
                    dest => dest.ResidentialOwnershipType, opt => opt.MapFrom(src => src.ResidentialOwnershipType.Id))
                .ForMember(
                    dest => dest.ResidentialType, opt => opt.MapFrom(src => src.ResidentialType.Id))
                .ForMember(
                    dest => dest.ResidentialStayStartYear,
                    opt => opt.MapFrom(src =>
                        (src.ResidentialStayStartYear != 0 ? src.ResidentialStayStartYear.ToString() : string.Empty)))
                .ForMember(
                    dest => dest.ResidentialLengthOfStay,
                    opt => opt.MapFrom(src =>
                        (src.ResidentialStayStartYear != 0 ? (DateTime.Now.Year - src.ResidentialStayStartYear) : 0)));

            Mapper.CreateMap<ICustomerVO, CustomerViewModel>()
                .Include<ICompanyCustomerVO, CompanyCustomerViewModel>()
                .Include<IIndividualCustomerVO, IndividualCustomerViewModel>()
                .ForMember(
                    dest => dest.IdType, opt => opt.MapFrom(src => src.IdType.Id))
                .ForMember(
                    dest => dest.IdIssuedCountry, opt => opt.MapFrom(src => src.IdIssuedCountry.Code))
                .ForMember(
                    dest => dest.Classification, opt => opt.MapFrom(src => src.Classification.Id))
                .ForMember(
                    dest => dest.RelationshipSince, 
                    opt => opt.MapFrom(src => (src.RelationshipSince != 0 ? src.RelationshipSince.ToString() : string.Empty)));

            Mapper.CreateMap<ICompanyCustomerVO, CompanyCustomerViewModel>()
                .ForMember(
                    dest => dest.AccountOpenedDate,
                    opt => opt.ResolveUsing<NullableDateTimeToStringResolver>().FromMember(x => x.AccountOpenedDate))
                .ForMember(
                    dest => dest.FacilityGrantedDate,
                    opt => opt.ResolveUsing<NullableDateTimeToStringResolver>().FromMember(x => x.FacilityGrantedDate))
                .ForMember(
                    dest => dest.Constitution, opt => opt.MapFrom(src => src.Constitution.Id))
                .ForMember(
                    dest => dest.MandatoryInfoNotAvailableInd,
                    opt => opt.ResolveUsing<StringToBoolResolver>().FromMember(x => x.MandatoryInfoNotAvailableInd))
                .ForMember(
                    dest => dest.IncorporationCountry, opt => opt.MapFrom(src => src.IncorporationCountry.Code))
                .ForMember(
                    dest => dest.IncorporationDate,
                    opt => opt.ResolveUsing<NullableDateTimeToStringResolver>().FromMember(x => x.IncorporationDate))
                .ForMember(
                    dest => dest.BusinessOperationCountry, opt => opt.MapFrom(src => src.BusinessOperationCountry.Code))
                .ForMember(
                    dest => dest.Address,
                    opt => opt.ResolveUsing<AddressViewResolver>().FromMember(x => x.AddressList));

            Mapper.CreateMap<IIndividualCustomerVO, IndividualCustomerViewModel>()
                .ForMember(
                    dest => dest.AccountOpenedDate,
                    opt => opt.ResolveUsing<NullableDateTimeToStringResolver>().FromMember(x => x.AccountOpenedDate))
                .ForMember(
                    dest => dest.FacilityGrantedDate,
                    opt => opt.ResolveUsing<NullableDateTimeToStringResolver>().FromMember(x => x.FacilityGrantedDate))
                .ForMember(
                    dest => dest.WithBankSinceDate,
                    opt => opt.ResolveUsing<NullableDateTimeToStringResolver>().FromMember(x => x.WithBankSinceDate))
                .ForMember(
                    dest => dest.FirstCreditGrantedDate,
                    opt => opt.ResolveUsing<NullableDateTimeToStringResolver>().FromMember(x => x.FirstCreditGrantedDate))
                .ForMember(
                    dest => dest.Salutation, opt => opt.MapFrom(src => src.Salutation.Id))
                .ForMember(
                    dest => dest.PreviousSalutation, opt => opt.MapFrom(src => src.PreviousSalutation.Id))
                .ForMember(
                    dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.Id))
                .ForMember(
                    dest => dest.MaritalStatus, opt => opt.MapFrom(src => src.MaritalStatus.Id))
                .ForMember(
                    dest => dest.BirthDate,
                    opt => opt.ResolveUsing<NullableDateTimeToStringResolver>().FromMember(x => x.BirthDate))
                .ForMember(
                    dest => dest.Age,
                    opt => opt.ResolveUsing<AgeResolver>().FromMember(x => x.BirthDate))
                .ForMember(
                    dest => dest.Nationality, opt => opt.MapFrom(src => src.Nationality.Code))
                .ForMember(
                    dest => dest.ResidentCountry, opt => opt.MapFrom(src => src.ResidentCountry.Code))
                .ForMember(
                    dest => dest.Race, opt => opt.MapFrom(src => src.Race.Id))
                .ForMember(
                    dest => dest.HighestEducation, opt => opt.MapFrom(src => src.HighestEducation.Id))
                .ForMember(
                    dest => dest.Address,
                    opt => opt.ResolveUsing<AddressViewResolver>().FromMember(x => x.AddressList));

            // Customer Details : Form to VO
            Mapper.CreateMap<RelationshipManager, IUserVO>().As<UserVO>();
            Mapper.CreateMap<RelationshipManager, UserVO>();

            Mapper.CreateMap<AddressDetailViewModel, IAddressVO>().As<AddressVO>();
            Mapper.CreateMap<AddressDetailViewModel, AddressVO>()
                .ForMember(
                    dest => dest.AddressFormat,
                    opt => opt.ResolveUsing<StringToCodeValueResolver>().FromMember(x => x.AddressFormat))
                .ForMember(
                    dest => dest.StructuredCountry,
                    opt => opt.ResolveUsing<StringToCountryResolver>().FromMember(x => x.StructuredCountry))
                .ForMember(
                    dest => dest.ResidentialOwnershipType,
                    opt => opt.ResolveUsing<StringToCodeValueResolver>().FromMember(x => x.ResidentialOwnershipType))
                .ForMember(
                    dest => dest.ResidentialType,
                    opt => opt.ResolveUsing<StringToCodeValueResolver>().FromMember(x => x.ResidentialType));

            Mapper.CreateMap<CompanyCustomerViewModel, CompanyCustomerVO>()
                // Base
                .ForMember(
                    dest => dest.IdType,
                    opt => opt.ResolveUsing<StringToCodeValueResolver>().FromMember(x => x.IdType))
                .ForMember(
                    dest => dest.IdIssuedCountry,
                    opt => opt.ResolveUsing<StringToCountryResolver>().FromMember(x => x.IdIssuedCountry))
                .ForMember(
                    dest => dest.Classification,
                    opt => opt.ResolveUsing<StringToCodeValueResolver>().FromMember(x => x.Classification))
                .ForMember(
                    dest => dest.AccountOpenedDate,
                    opt => opt.ResolveUsing<StringToNullableDateTimeResolver>().FromMember(x => x.AccountOpenedDate))
                .ForMember(
                    dest => dest.FacilityGrantedDate,
                    opt => opt.ResolveUsing<StringToNullableDateTimeResolver>().FromMember(x => x.FacilityGrantedDate))
                .ForMember(
                    dest => dest.AddressList,
                    opt => opt.ResolveUsing<AddressListResolver>()
                        .FromMember(x => x.Address)
                        .ConstructedBy(() => new AddressListResolver(CustomerType.Corporate)))
                // Actual
                .ForMember(
                    dest => dest.Constitution,
                    opt => opt.ResolveUsing<StringToCodeValueResolver>().FromMember(x => x.Constitution))
                .ForMember(
                    dest => dest.MandatoryInfoNotAvailableInd,
                    opt => opt.ResolveUsing<BoolToStringResolver>().FromMember(x => x.MandatoryInfoNotAvailableInd))
                .ForMember(
                    dest => dest.IncorporationCountry,
                    opt => opt.ResolveUsing<StringToCountryResolver>().FromMember(x => x.IncorporationCountry))
                .ForMember(
                    dest => dest.IncorporationDate,
                    opt => opt.ResolveUsing<StringToNullableDateTimeResolver>().FromMember(x => x.IncorporationDate))
                .ForMember(
                    dest => dest.BusinessOperationCountry,
                    opt => opt.ResolveUsing<StringToCountryResolver>().FromMember(x => x.BusinessOperationCountry));

            Mapper.CreateMap<IndividualCustomerViewModel, IndividualCustomerVO>()
                // Base
                .ForMember(
                    dest => dest.IdType,
                    opt => opt.ResolveUsing<StringToCodeValueResolver>().FromMember(x => x.IdType))
                .ForMember(
                    dest => dest.IdIssuedCountry,
                    opt => opt.ResolveUsing<StringToCountryResolver>().FromMember(x => x.IdIssuedCountry))
                .ForMember(
                    dest => dest.Classification,
                    opt => opt.ResolveUsing<StringToCodeValueResolver>().FromMember(x => x.Classification))
                .ForMember(
                    dest => dest.AccountOpenedDate,
                    opt => opt.ResolveUsing<StringToNullableDateTimeResolver>().FromMember(x => x.AccountOpenedDate))
                .ForMember(
                    dest => dest.FacilityGrantedDate,
                    opt => opt.ResolveUsing<StringToNullableDateTimeResolver>().FromMember(x => x.FacilityGrantedDate))
                .ForMember(
                    dest => dest.AddressList,
                    opt => opt.ResolveUsing<AddressListResolver>()
                        .FromMember(x => x.Address)
                        .ConstructedBy(() => new AddressListResolver(CustomerType.Individual)))
                // Actual
                .ForMember(
                    dest => dest.WithBankSinceDate,
                    opt => opt.ResolveUsing<StringToNullableDateTimeResolver>().FromMember(x => x.WithBankSinceDate))
                .ForMember(
                    dest => dest.FirstCreditGrantedDate,
                    opt => opt.ResolveUsing<StringToNullableDateTimeResolver>().FromMember(x => x.FirstCreditGrantedDate))
                .ForMember(
                    dest => dest.Salutation,
                    opt => opt.ResolveUsing<StringToCodeValueResolver>().FromMember(x => x.Salutation))
                .ForMember(
                    dest => dest.PreviousSalutation,
                    opt => opt.ResolveUsing<StringToCodeValueResolver>().FromMember(x => x.PreviousSalutation))
                .ForMember(
                    dest => dest.Gender,
                    opt => opt.ResolveUsing<StringToCodeValueResolver>().FromMember(x => x.Gender))
                .ForMember(
                    dest => dest.MaritalStatus,
                    opt => opt.ResolveUsing<StringToCodeValueResolver>().FromMember(x => x.MaritalStatus))
                .ForMember(
                    dest => dest.BirthDate,
                    opt => opt.ResolveUsing<StringToNullableDateTimeResolver>().FromMember(x => x.BirthDate))
                .ForMember(
                    dest => dest.Nationality,
                    opt => opt.ResolveUsing<StringToCountryResolver>().FromMember(x => x.Nationality))
                .ForMember(
                    dest => dest.ResidentCountry,
                    opt => opt.ResolveUsing<StringToCountryResolver>().FromMember(x => x.ResidentCountry))
                .ForMember(
                    dest => dest.Race,
                    opt => opt.ResolveUsing<StringToCodeValueResolver>().FromMember(x => x.Race))
                .ForMember(
                    dest => dest.HighestEducation,
                    opt => opt.ResolveUsing<StringToCodeValueResolver>().FromMember(x => x.HighestEducation));
        }
    }

    public class AddressViewResolver : ValueResolver<IList<IAddressVO>, AddressViewModel>
    {
        protected override AddressViewModel ResolveCore(IList<IAddressVO> source)
        {
            AddressViewModel addressModel = new AddressViewModel();
            if (source != null && source.Count > 0)
            {
                foreach (IAddressVO address in source)
                {
                    AddressDetailViewModel addressDetailModel = Mapper.Map<AddressDetailViewModel>(address);

                    if (address.AddressType == Constants.GetEnumDescription(AddressType.Home))
                    {
                        addressModel.HomeAddress = addressDetailModel;
                    }
                    else if (address.AddressType == Constants.GetEnumDescription(AddressType.Business))
                    {
                        addressModel.RegisteredAddress = addressDetailModel;
                    }
                    else if (address.AddressType == Constants.GetEnumDescription(AddressType.Mailing))
                    {
                        addressModel.MailingAddress = addressDetailModel;
                    }
                }
            }

            return addressModel;
        }
    }

    public class AddressListResolver : ValueResolver<AddressViewModel, IList<IAddressVO>>
    {
        private CustomerType CustomerType { get; set; }

        public AddressListResolver(CustomerType customerType)
        {
            CustomerType = customerType;
        }

        protected override IList<IAddressVO> ResolveCore(AddressViewModel source)
        {
            IList<IAddressVO> addressList = new List<IAddressVO>();

            if (source != null)
            {
                if (CustomerType == CustomerType.Individual)
                {
                    IAddressVO homeAddress = Mapper.Map<AddressVO>(source.HomeAddress);
                    homeAddress.AddressType = Constants.GetEnumDescription(AddressType.Home);
                    addressList.Add(homeAddress);
                }
                else
                {
                    IAddressVO businessAddress = Mapper.Map<AddressVO>(source.RegisteredAddress);
                    businessAddress.AddressType = Constants.GetEnumDescription(AddressType.Business);
                    addressList.Add(businessAddress);
                }

                IAddressVO mailAddress = Mapper.Map<AddressVO>(source.MailingAddress);
                mailAddress.AddressType = Constants.GetEnumDescription(AddressType.Mailing);
                addressList.Add(mailAddress);
            }

            return addressList;
        }
    }
}