using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SignalR_App.Application.Dtos.Dtos;
using SignalR_App.Application.Mappings;
using SignalR_App.Application.Repositories;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.WebServices
{
    public class WebService(IRepository<Slider, int> sliderRepository,
        IMemoryCache cacheManager) : IWebService
    {
        private readonly IRepository<Slider, int> _sliderRepository = sliderRepository;
        private readonly IMemoryCache _cacheManager = cacheManager;

        public async Task<List<SliderDto>> GetAllSliders()
        {
            return await _cacheManager.GetOrCreateAsync("Sliders", async entry =>
            {
                var sliders = await _sliderRepository.GetAll().Where(c => c.Status == Domain.Enums.Status.Active).ToListAsync();
                var map = ObjectMapper.Map.Map<List<SliderDto>>(sliders);
                return map;
            }) ?? new();
        }


    }
}
