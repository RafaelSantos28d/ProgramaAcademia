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
            if (userExist is null)
            {
                throw new NotFoundException($"Usuário com e-mail '{email}' não encontrado.");
            }

            var result = await _userManager.AddToRoleAsync(userExist, roleName);

            if (!result.Succeeded)
            {
                return new ResponseModel
                {
                    Status = "Error",
                    Message = "Não foi possível adicionar o usuário à role.",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            return new ResponseModel
            {
                Status = "Success",
                Message = $"Usuário '{email}' adicionado à role '{roleName}' com sucesso."
            };
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

            if (!result.Succeeded)
            {
                return new ResponseModel
                {
                    Status = "Error",
                    Message = "Não foi possível cadastrar o usuário.",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            await _userManager.SetLockoutEnabledAsync(aplicationUser, false);

            return new ResponseModel
            {
                Status = "Success",
                Message = "Usuário cadastrado com sucesso."
            };
        }

        public async Task<ResponseModel> CreateRole(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (roleExist)
            {
                throw new BadRequestException($"A role '{roleName}' já existe.");
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (!result.Succeeded)
            {
                return new ResponseModel
                {
                    Status = "Error",
                    Message = "Não foi possível criar a role.",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            return new ResponseModel
            {
                Status = "Success",
                Message = $"Role '{roleName}' criada com sucesso."
            };
        }

        public async Task<TokenModel> Login(LoginModel login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName!);

            if (user is null || !await _userManager.CheckPasswordAsync(user, login.Password!))
            {
                throw new NotFoundException("Usuário ou senha inválidos.");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            if (!userRoles.Contains("Admin") && !userRoles.Contains("Employee"))
            {
                throw new UnauthorizedAccessException("Usuário não possui permissão para acessar o sistema.");

            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in userRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            var token = _tokenService.GenerateAccessToken(authClaims, _configuration);
            var refreshToken = _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_configuration["Jwt:RefreshTokenValidityMinutes"], out int refreshTokenValidityToken);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityToken);
            await _userManager.UpdateAsync(user);

            return new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken
            };
        }
    }
}