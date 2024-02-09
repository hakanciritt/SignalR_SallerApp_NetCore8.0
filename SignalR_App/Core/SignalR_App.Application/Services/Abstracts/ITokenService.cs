using SignalR_App.Application.Dtos.TokenDtos;
using SignalR_App.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR_App.Application.Services.Abstracts;

public interface ITokenService
{
    Task<TokenDto> CreateAccessToken(AppUser user);

}

