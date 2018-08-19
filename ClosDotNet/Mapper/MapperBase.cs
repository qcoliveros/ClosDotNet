namespace ClosDotNet.Mapper
{
    using AutoMapper;
    using ClosDotNet.Domain.CodeSet;
    using ClosDotNet.Domain.User;
    using ClosDotNet.Filters;
    using System;
    using System.Web.Mvc;

    public interface IMapper
    {
        object Map(object source, Type sourceType, Type destinationType);

        object Map(object source, object destination);
    }

    public class MapperBase : IMapper
    {
        public MapperBase()
        {
            Mapper.CreateMap<ICountryVO, SelectListItem>()
                .ForMember(
                    dest => dest.Value, opt => opt.MapFrom(src => src.Code))
                .ForMember(
                    dest => dest.Text, opt => opt.MapFrom(src => src.Description));

            Mapper.CreateMap<ICurrencyVO, SelectListItem>()
                .ForMember(
                    dest => dest.Value, opt => opt.MapFrom(src => src.Code))
                .ForMember(
                    dest => dest.Text, opt => opt.MapFrom(src => src.Code));

            Mapper.CreateMap<ICodeValueVO, SelectListItem>()
                .ForMember(
                    dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.Text, opt => opt.MapFrom(src => src.Description));

            Mapper.CreateMap<IUserVO, SelectListItem>()
                .ForMember(
                    dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.Text, opt => opt.MapFrom(src => src.FullName));

            Mapper.CreateMap<IActionTypeVO, SelectListItem>()
                .ForMember(
                    dest => dest.Value, opt => opt.MapFrom(src => src.Code))
                .ForMember(
                    dest => dest.Text, opt => opt.MapFrom(src => src.Description));
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }

        public object Map(object source, object destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}