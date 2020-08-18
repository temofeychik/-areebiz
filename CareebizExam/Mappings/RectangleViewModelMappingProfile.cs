using AutoMapper;
using CareebizExam.Models;
using CareebizExam.WebApi.ViewModels;

namespace CareebizExam.WebApi.Mappings
{
    public class RectangleViewModelMappingProfile : Profile
    {
        public RectangleViewModelMappingProfile()
        {
            CreateMap<RectangleViewModel, Rectangle>()
                .ForMember(mem => mem.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(mem => mem.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(mem => mem.East, opt => opt.MapFrom(src => src.East))
                .ForMember(mem => mem.North, opt => opt.MapFrom(src => src.North))
                .ForMember(mem => mem.South, opt => opt.MapFrom(src => src.South))
                .ForMember(mem => mem.West, opt => opt.MapFrom(src => src.West));
        }
    }
}
