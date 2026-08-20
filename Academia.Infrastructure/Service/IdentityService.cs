using Academia.Application.DTOs.User;
using Academia.Application.Interfaces;
using Academia.Domain.Entities;
using Academia.Domain.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<ResponseModel> AddRoleToUser(string email, string roleName)
        {
            var userExist = await _userManager.FindByEmailAsync(email);
            if(userExist != null)
            {
                var result = await _userManager.AddToRoleAsync(userExist, roleName);
                if(result.Succeeded)
                {
                    var response = new ResponseModel
                    {
                        Status = result.Succeeded.ToString(),
                        Message = $"User {email} added to {roleName} role"
                    };
                    return response;
                }
                else
                {

                    var response = new ResponseModel
                    {
                        Status = "Error",
                        Message = "Unable to add user to role"
                    };
                    return response;
                }
            }
            throw new BadRequestException("Unable to find user");
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

        public async Task<ResponseModel> CreateRole(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if(!roleExist)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if(result.Succeeded)
                {
                    var response = new ResponseModel
                    {
                        Status = result.Succeeded.ToString(),
                        Message = "Usuario cadastrado com sucesso"
                    };
                    return response;
                }
                else
                {

                    var response = new ResponseModel
                    {
                        Status = "Error",
                        Message = "Erro ao cadastrar role"
                    };
                    return response;
                }
            }

            throw new BadRequestException("Role already exist");
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
                if (!userRoles.Contains("Admin") && !userRoles.Contains("Employee"))
                {
                    throw new UnauthorizedAccessException(
                        "Usuário não possui permissão para acessar o sistema."
                    );
                }
                foreach (var role in  userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role,role));
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

            throw new NotFoundException("Usuário não encontrado");
           
        }
        
        
    }
}
