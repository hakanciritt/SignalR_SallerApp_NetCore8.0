using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SignalR_App.Application.Dtos.AuthenticateDtos;
using SignalR_App.Application.Services.Abstracts;
using SignalR_App.Domain.Entitites;
using SignalR_App.Domain.Results;

namespace SignalR_App.Application.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public async Task<DataResult<AppUser>> CreateUser(RegisterDto register)
        {
            var user = new AppUser()
            {
                Name = register.Name,
                SurName = register.Surname,
                Email = register.Email,
                UserName = register.Email,
                EmailConfirmed = true,
            };

            var emailCheck = await _userManager.FindByEmailAsync(register.Email);
            if (emailCheck != null)
            {
                return DataResult<AppUser>.Failed("Bu mail adresiyle kayıtlı bir kullanıcı saten mevcut.");
            }

            var registerResult = await _userManager.CreateAsync(user, register.Password);

            if (!registerResult.Succeeded)
            {
                return DataResult<AppUser>.Failed(registerResult.Errors.Select(c => c.Description).ToList());
            }

            var findUser = await _userManager.FindByEmailAsync(user.Email);
            if (findUser is null)
            {
                return DataResult<AppUser>.Failed("Kullanıcı bulunamadı.");
            }

            return DataResult<AppUser>.Successed(findUser, "Kullanıcı başarılı bir şekilde kayıt edildi");
        }

        public async Task<DataResult<AppUser>> Login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user is null) return DataResult<AppUser>.Failed("Kullanıcı bulunamadı.");

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (!signInResult.Succeeded)
            {
                return DataResult<AppUser>.Failed("Kullanıcı adı veya şifre hatalı");
            }

            return DataResult<AppUser>.Successed(user);
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
