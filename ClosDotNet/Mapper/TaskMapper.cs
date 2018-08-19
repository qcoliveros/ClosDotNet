namespace ClosDotNet.Mapper
{
    using AutoMapper;
    using ClosDotNet.Domain.CallReport;
    using ClosDotNet.Filters;
    using ClosDotNet.Models;

    public class TaskMapper : MapperBase
    {
        public TaskMapper()
            : base()
        {
            // Call Report Task List
            Mapper.CreateMap<ICallReportVO, CallReportTaskViewModel>()
                .ForMember(
                    dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
                .ForMember(
                    dest => dest.CustomerType, opt => opt.MapFrom(src => src.Customer.CustomerType))
                .ForMember(
                    dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.CustomerName))
                .ForMember(
                    dest => dest.OriginatorName, opt => opt.MapFrom(src => src.Owner.FullName))
                .ForMember(
                    dest => dest.LastRecipientName, opt => opt.MapFrom(src => src.CurrentRecipient.FullName))
                .ForMember(
                    dest => dest.CallReportId, opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.CallDate,
                    opt => opt.ResolveUsing<NullableDateTimeToStringResolver>().FromMember(x => x.CallDate))
                .ForMember(
                    dest => dest.CallPurpose, opt => opt.MapFrom(src => src.CallPurpose.Description))
                .ForMember(
                    dest => dest.Workflow, opt => opt.MapFrom(src => src.ProcessDefinition));
        }
    }
}