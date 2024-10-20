using SignalR_App.Application.Dtos.TokenDtos;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Services.Abstracts;

public interface ITokenService
{
    Task<TokenDto> CreateAccessToken(AppUser user);

}

