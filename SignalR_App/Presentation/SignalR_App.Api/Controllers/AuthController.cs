using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application.Dtos.AuthenticateDtos;
using SignalR_App.Application.Services.Abstracts;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Api.Controllers
{
    public class AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, 
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var loginResult = await _userService.Login(login);
            if (loginResult.Success)
            {
                var token = await _tokenService.CreateAccessToken(loginResult.Data);
                return Ok(token);
            }

            return BadRequest(loginResult);
        }
    }
}
