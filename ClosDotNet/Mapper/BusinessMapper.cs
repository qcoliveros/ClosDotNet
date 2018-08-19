namespace ClosDotNet.Mapper
{
    using AutoMapper;
    using ClosDotNet.Domain.Base.Model;
    using ClosDotNet.Domain.Business;
    using ClosDotNet.Domain.CodeSet;
    using ClosDotNet.Domain.User;
    using ClosDotNet.Models;
    using System;
    using System.Globalization;
    using ClosDotNet.Filters;

    public class BusinessMapper : MapperBase
    {
        public BusinessMapper()
            : base()
        {
            // Business Details : VO to Form
            Mapper.CreateMap<IBusinessVO, BusinessViewModel>()
                .ForMember(
                    dest => dest.CurrentPaidUpCapitalCurrency, opt => opt.MapFrom(src => src.CurrentPaidUpCapital.Currency.Code))
                .ForMember(
                    dest => dest.CurrentPaidUpCapitalAmount,
                    opt => opt.MapFrom(src => string.Format("{0:n}", src.CurrentPaidUpCapital.Value)));

            // Business Details : Form to VO
            Mapper.CreateMap<BusinessViewModel, BusinessVO>()
                .ForMember(
                    dest => dest.CurrentPaidUpCapital,
                    opt => opt.MapFrom(src =>
                        ((!string.IsNullOrEmpty(src.CurrentPaidUpCapitalCurrency) && !string.IsNullOrEmpty(src.CurrentPaidUpCapitalAmount)) 
                            ? new AmountVO(src.CurrentPaidUpCapitalCurrency, src.CurrentPaidUpCapitalAmount) : null)));
        }
    }
}