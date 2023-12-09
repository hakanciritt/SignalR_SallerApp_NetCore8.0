using AutoMapper;
using SignalR_App.Application.Dtos.TestimonialDtos;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Mappings
{
    public class TestimonialMap : Profile
    {
        public TestimonialMap()
        {
            CreateMap<Testimonial, TestimonialDto>().ReverseMap(); 
        }
    }
}
