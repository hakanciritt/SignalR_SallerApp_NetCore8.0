using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application.Dtos.AuthenticateDtos;
using SignalR_App.Application.Services.Abstracts;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService,
        IUserService userService) : ApiControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IUserService _userService = userService;

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            var registerResult = await _userService.CreateUser(register);

            if (registerResult.Success)
            {
                var token = await _tokenService.CreateAccessToken(registerResult.Data);
                return Ok(token);
            }

            return BadRequest(registerResult);
        }
    }
}
