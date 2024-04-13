using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductManagementSystem.Application.Dtos;
using ProductManagementSystem.Domain.Users;
using ProductManagementSystem.Infrastructure.Helpers.Tokens;
using ProductManagementSystem.Shared.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(UserManager<AppUser> userManager, JwtTokenGenerator jwtTokenGenerator, IConfiguration _config) : ControllerBase
    {

        // POST: api/AccountController/Authenticate
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null) return Unauthorized();

            JwtSecurityToken token = jwtTokenGenerator.GenerateJwt(user.UserName);

            var refreshToken = JwtTokenGenerator.GenerateRefreshToken();

            user.RefreshTokenExpiry = DateTime.UtcNow.AddMinutes(90);
            user.RefreshToken = refreshToken;

            await userManager.UpdateAsync(user);

            return Ok(new AuthenticateResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] AuthenticateRefresh model)
        {
            var principal = GetPrincipalFromExpiredToken(model.AccessToken);

            if (principal?.Identity?.Name is null)
                return Unauthorized();

            var user = await userManager.FindByNameAsync(principal.Identity.Name);

            if (user is null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
                return Unauthorized();

            var token = jwtTokenGenerator.GenerateJwt(principal.Identity.Name);

            return Ok(new AuthenticateResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                RefreshToken = model.RefreshToken
            });
        }

        [Authorize]
        [HttpDelete("revoke")]
        public async Task<IActionResult> Revoke()
        {

            var username = HttpContext.User.Identity?.Name;

            if (username is null)
                return Unauthorized();

            var user = await userManager.FindByNameAsync(username);

            if (user is null)
                return Unauthorized();

            user.RefreshToken = null;

            await userManager.UpdateAsync(user);


            return Ok();
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var secret = _config["AuthSettings:Secret"] ?? throw new InvalidOperationException("Secret not configured");

            var validation = new TokenValidationParameters
            {
                ValidIssuer = "test",
                ValidAudience = "test",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                ValidateLifetime = false
            };

            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }
    }
}
