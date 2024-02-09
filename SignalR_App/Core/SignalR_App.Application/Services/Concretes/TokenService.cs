using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SignalR_App.Application.Dtos.TokenDtos;
using SignalR_App.Application.Services.Abstracts;
using SignalR_App.Domain.Entitites;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SignalR_App.Application.Services.Concretes
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public TokenService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<TokenDto> CreateAccessToken(AppUser user)
        {
            var expireDate = DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:Minutes"]));

            var securityToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: await GetUserClaims(user),
                expires: expireDate,
                signingCredentials: SignInCredentials()
                );

            var tokenResult = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return new()
            {
                Token = tokenResult,
                Expiration = expireDate
            };
        }

        private async Task<List<Claim>> GetUserClaims(AppUser user)
        {
            var claims = (await _userManager.GetClaimsAsync(user)) ?? new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            claims.Add(new Claim(ClaimTypes.Surname, user.SurName));

            return claims.ToList();
        }
        private SigningCredentials SignInCredentials()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
