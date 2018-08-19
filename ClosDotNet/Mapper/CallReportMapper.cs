namespace ClosDotNet.Mapper
{
    using AutoMapper;
    using ClosDotNet.Domain.CallReport;
    using ClosDotNet.Domain.CodeSet;
    using ClosDotNet.Filters;
    using ClosDotNet.Models;

    public class CallReportMapper : MapperBase
    {
        public CallReportMapper()
            : base()
        {
            // Call Report List
            Mapper.CreateMap<ICallReportVO, ListCallReportViewModel>()
                .ForMember(
                    dest => dest.CallPurpose, opt => opt.MapFrom(src => src.CallPurpose.Description))
                .ForMember(
                    dest => dest.CallDate,
                    opt => opt.ResolveUsing<NullableDateTimeToStringResolver>().FromMember(x => x.CallDate))
                .ForMember(
                    dest => dest.Originator, opt => opt.MapFrom(src => src.Owner.FullName))
                .ForMember(
                    dest => dest.LastUpdateDate,
                    opt => opt.ResolveUsing<NullableDateTimeToStringResolver>().FromMember(x => x.LastUpdateDate))
                .ForMember(
                    dest => dest.CurrentRecipient, opt => opt.MapFrom(src => src.CurrentRecipient.FullName))
                .ForMember(
                    dest => dest.Workflow, opt => opt.MapFrom(src => src.ProcessDefinition));

            // Call Report Details : VO to Form
            Mapper.CreateMap<ICallReportVO, CallReportViewModel>()
                .ForMember(
                    dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.CustomerName))
                .ForMember(
                    dest => dest.PreviousCallDate,
                    opt => opt.ResolveUsing<NullableDateTimeToStringResolver>().FromMember(x => x.PreviousCallDate))
                .ForMember(
                    dest => dest.CallDate,
                    opt => opt.ResolveUsing<NullableDateTimeToStringResolver>().FromMember(x => x.CallDate))
                .ForMember(
                    dest => dest.SiteVisitDate,
                    opt => opt.ResolveUsing<NullableDateTimeToStringResolver>().FromMember(x => x.SiteVisitDate))
                .ForMember(
                    dest => dest.CallPurpose, opt => opt.MapFrom(src => src.CallPurpose.Id));

            // Call Report Details : Form to VO
            Mapper.CreateMap<CallReportViewModel, CallReportVO>()
                .ForMember(
                    dest => dest.CallDate,
                    opt => opt.ResolveUsing<StringToNullableDateTimeResolver>().FromMember(x => x.CallDate))
                .ForMember(
                    dest => dest.SiteVisitDate,
                    opt => opt.ResolveUsing<StringToNullableDateTimeResolver>().FromMember(x => x.SiteVisitDate))
                .ForMember(
                    dest => dest.CallPurpose, opt => opt.MapFrom(src => new CodeValueVO { Id = src.CallPurpose }))
                .ForMember(
                    dest => dest.Reviewer,
                    opt => opt.ResolveUsing<StringToUserResolver>().FromMember(x => x.ReviewerId));

        }
    }
}