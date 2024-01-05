using SignalR_App.Application.Dtos.AuthenticateDtos;
using SignalR_App.Domain.Entitites;
using SignalR_App.Domain.Results;

namespace SignalR_App.Application.Services.Abstracts
{
    public interface IUserService
    {
        Task<DataResult<AppUser>> CreateUser(RegisterDto register);
        Task<DataResult<AppUser>> Login(LoginDto login);
    }
}
