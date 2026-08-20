using Academia.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Academia.Infrastructure.IdentityService
{
    public class TokenService : ITokenService
    {
        public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _config)
        {
            var privatKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var signingCredentials = new SigningCredentials(privatKey,SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:TokenValidityMinutes"])),
                SigningCredentials = signingCredentials,
                Issuer = _config["Jwt:ValidIssuer"],
                Audience = _config["Jwt:ValidAudience"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);


            return token;
        }

        public string GenerateRefreshToken()
        {
            var secureRandomBytes = new byte[128];
            using var random = RandomNumberGenerator.Create();
            random.GetBytes(secureRandomBytes);
            var refresToken = Convert.ToBase64String(secureRandomBytes);
            return refresToken;
        }

        public ClaimsPrincipal GetClaimsPrincipal(string token, IConfiguration _conig)
        {
            var secretKey = _conig["Jwt:SecretKey"] ?? throw new InvalidOperationException("Invalid secret key");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidateLifetime = false,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if(securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase
                ))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
