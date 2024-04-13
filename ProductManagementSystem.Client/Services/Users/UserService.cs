using Blazored.SessionStorage;
using ProductManagementSystem.Application.Dtos;
using ProductManagementSystem.Client.Interfaces.Users;
using ProductManagementSystem.Shared.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ProductManagementSystem.Client.Services.Users
{
    public class UserService(IHttpClientFactory httpClient, ISessionStorageService sessionStorageService) : IUserService
    {
        private readonly IHttpClientFactory _httpClient = httpClient;
        private readonly ISessionStorageService _sessionStorageService = sessionStorageService;
        private const string jwt_key = nameof(jwt_key);
        private string _jwtCache;
        private const string refresh_Key = nameof(refresh_Key);
        public event Action<string?>? LoginChange;

        public async Task LogoutAsync()
        {
            var response = await _httpClient.CreateClient("ServerApi").DeleteAsync("api/Users/revoke");

            await _sessionStorageService.RemoveItemAsync(jwt_key);
            await _sessionStorageService.RemoveItemAsync(refresh_Key);

            _jwtCache = null;

            await Console.Out.WriteLineAsync($"Revoke gave response {response.StatusCode}");

            LoginChange?.Invoke(null);
        }

        public string GetUsername(string token)
        {
            var jwt = new JwtSecurityToken(token);

            return jwt.Claims.First(c => c.Type == ClaimTypes.Name).Value;
        }

        public async Task<DateTime> Authenticate(AuthenticateRequest model)
        {
            var response = await _httpClient.CreateClient("ServerApi").PostAsJsonAsync("api/Users/authenticate", model);

            if (!response.IsSuccessStatusCode)
                throw new UnauthorizedAccessException("Login failed.");

            var content = await response.Content.ReadFromJsonAsync<AuthenticateResponse>() ?? throw new InvalidDataException();
            await _sessionStorageService.SetItemAsync(jwt_key, content.Token);
            await _sessionStorageService.SetItemAsync(refresh_Key, content.RefreshToken);

            LoginChange?.Invoke(GetUsername(content.Token));

            return content.Expiration;
        }

        public async Task<bool> RefreshAsync()
        {
            var model = new AuthenticateRefresh
            {
                AccessToken = await _sessionStorageService.GetItemAsync<string>(jwt_key),
                RefreshToken = await _sessionStorageService.GetItemAsync<string>(refresh_Key)
            };

            var response = await _httpClient.CreateClient("ServerApi").PostAsJsonAsync("api/authentication/refresh",model);

            if (!response.IsSuccessStatusCode)
            {
                await LogoutAsync();

                return false;
            }

            var content = await response.Content.ReadFromJsonAsync<AuthenticateResponse>() ?? throw new InvalidDataException();
            await _sessionStorageService.SetItemAsync(jwt_key, content.Token);
            await _sessionStorageService.SetItemAsync(refresh_Key, content.RefreshToken);

            _jwtCache = content.Token;

            return true;
        }

        public async Task<string> GetJwtAsync()
        {
            if (string.IsNullOrEmpty(_jwtCache))
                _jwtCache = await _sessionStorageService.GetItemAsync<string>(jwt_key);

            return _jwtCache;
        }

        public async Task<bool> IsAuthenticated()
        {
            bool IsAuthenticated = true;

            var token = await GetJwtAsync();
            if (string.IsNullOrEmpty(token))
                return false;

            string _username = GetUsername(token);
            if(string.IsNullOrEmpty(_username))
                return false;

            return IsAuthenticated;

        }
    }
}
