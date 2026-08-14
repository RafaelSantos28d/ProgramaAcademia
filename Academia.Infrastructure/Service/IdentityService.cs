using Academia.Application.DTOs.User;
using Academia.Application.Interfaces;
using Academia.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Academia.Infrastructure.IdentityService
{
    public class IdentityService : IIdentityservice
    {
        private readonly UserManager<AplicationUser> _userManager;
      
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        public IdentityService(UserManager<AplicationUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public async Task<ResponseModel> CadastrarUsuario(RegisterModel register)
        {
           
            AplicationUser aplicationUser = new()
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.UserName,
            };

            var result = await _userManager.CreateAsync(aplicationUser, register.Password);
            var response = new ResponseModel
            {
                Status = result.Succeeded.ToString(),
                Message = "Usuario cadastrado com sucesso"
            };

            if(result.Succeeded)
            {
                await _userManager.SetLockoutEnabledAsync(aplicationUser, false);
            }
            if (!result.Succeeded )
            {
                response.Status = "Error";
                response.Message = "Erro ao cadastrar usuario";
            }

            return response;
        }

        public async Task<TokenModel> Login(LoginModel login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName!);
            if (user != null && await _userManager.CheckPasswordAsync(user,login.Password!))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
                foreach(var roles in  userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role,userRoles.ToString()));
                }
                var token = _tokenService.GenerateAccessToken(authClaims,_configuration);
                var refreshToken = _tokenService.GenerateRefreshToken();
                _ = int.TryParse(_configuration["Jwt:RefreshTokenValidityMinutes"],
                    out int refreshTokenValidityToken);
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityToken);

                await _userManager.UpdateAsync(user);

                return new TokenModel
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken
                };

            }

            throw new InvalidOperationException("Erro");
           
        }
        
        
    }
}
