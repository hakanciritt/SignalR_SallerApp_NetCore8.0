using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SignalR_App.Application.Dtos.AuthenticateDtos;
using SignalR_App.Application.Dtos.TokenDtos;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;

namespace SignalR_App.WebUI.Areas.Admin.Controllers
{
    public class UserController(IConfiguration configuration,
        IHttpClientFactory httpClientFactory) : AdminBaseController
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
            var client = _httpClientFactory.CreateClient("Auth");
            await client.PostAsync("Auth/Logout", null);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var client = _httpClientFactory.CreateClient("Auth");

            var content = new StringContent(JsonSerializer.Serialize(login), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Auth/Login", content);
            if (!response.IsSuccessStatusCode) return View();

            var tokenDto = await response.Content.ReadFromJsonAsync<TokenDto>();
            if (tokenDto is null) return View();

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var securityKey = _configuration["Jwt:SecurityKey"];

            var token = new JwtSecurityTokenHandler();
            var tokenParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidAudience = audience,
                ValidIssuer = issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))
            };

            var claimsPrincipal = token.ValidateToken(tokenDto.Token, tokenParameters, out SecurityToken validatedToken);
            if (validatedToken is not null)
            {

                await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, claimsPrincipal, new()
                {
                    ExpiresUtc = tokenDto.Expiration,
                });
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
