using Microsoft.EntityFrameworkCore;
using SignalR_App.Application.Dtos.SettingDtos;
using SignalR_App.Application.Mappings;
using SignalR_App.Application.Repositories;
using SignalR_App.Application.Services.Abstracts;
using SignalR_App.Domain.Entitites;
using SignalR_App.Domain.Result;
using SignalR_App.Domain.Results;

namespace SignalR_App.Application.Services.Concretes
{
    public class SettingService(IRepository<Setting, int> settingRepository) : ISettingService
    {
        private readonly IRepository<Setting, int> _settingRepository = settingRepository;
        public async Task<List<SettingDto>> GetAll()
        {
            var result = await _settingRepository.GetAll().ToListAsync();
            return ObjectMapper.Map.Map<List<SettingDto>>(result);
        }
        public async Task<DataResult<SettingDto>> GetById(int id)
        {
            var result = await _settingRepository.GetAll().FirstOrDefaultAsync(c => c.Id == id);
            if (result is null) return DataResult<SettingDto>.Failed("Setting bulunamadı");

            return DataResult<SettingDto>.Successed(ObjectMapper.Map.Map<SettingDto>(result));
        }
        public async Task<Result> Update(SettingDto testimonial)
        {
            var result = await _settingRepository.GetAll().FirstOrDefaultAsync(c => c.Id == testimonial.Id);
            ObjectMapper.Map.Map(testimonial, result);
            await _settingRepository.SaveChangesAsync();

            return Result.Successed("Başarıyla güncellendi");
        }
    }
}
