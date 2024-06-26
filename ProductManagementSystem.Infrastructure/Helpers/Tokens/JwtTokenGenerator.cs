﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProductManagementSystem.Infrastructure.Helpers.Tokens
{
    public class JwtTokenGenerator(IConfiguration _config) 
    {
        public JwtSecurityToken GenerateJwt(string username)
        {
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, username),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _config["AuthSettings:Secret"]) ?? throw new InvalidOperationException("Secret not configured"));

            var token = new JwtSecurityToken(
                issuer: _config["ApiURL:Url"],
                audience: _config["ApiURL:Url"],
                expires: DateTime.UtcNow.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using var generator = RandomNumberGenerator.Create();

            generator.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
