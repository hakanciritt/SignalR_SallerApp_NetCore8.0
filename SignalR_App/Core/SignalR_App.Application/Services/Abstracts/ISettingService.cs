using SignalR_App.Application.Dtos.SettingDtos;
using SignalR_App.Domain.Result;
using SignalR_App.Domain.Results;

namespace SignalR_App.Application.Services.Abstracts
{
    public interface ISettingService
    {
        Task<List<SettingDto>> GetAll();
        Task<Result> Update(SettingDto setting);
        Task<DataResult<SettingDto>> GetById(int id);
    }
}
