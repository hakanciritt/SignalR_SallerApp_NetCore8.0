using SignalR_App.Application.Repositories;
using SignalR_App.Application.Services.Abstracts;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Services.Concretes
{
    public class SliderService(IRepository<Slider, int> sliderRepository) : ISliderService
    {
        private readonly IRepository<Slider, int> _sliderRepository = sliderRepository;




    }
}
