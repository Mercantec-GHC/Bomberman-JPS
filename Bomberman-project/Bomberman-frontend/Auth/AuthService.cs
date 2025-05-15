using System.Security.Claims;
using Blazored.LocalStorage;
using DomainModels;
using DomainModels.DTO;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Session;
using Microsoft.IdentityModel.JsonWebTokens;
using static System.Net.WebRequestMethods;
using System.Net.Http.Headers;

public delegate void AuthenticationChanged(AuthenticationState authenticationState);

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task SetAuthorHeader()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (token != null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<TokenResponse> Login(LoginDTO loginDTO)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/Player/login", loginDTO);
        if (response.IsSuccessStatusCode)
        {
            var token = await response.Content.ReadFromJsonAsync<TokenResponse>();
            await _localStorage.SetItemAsync("authToken", token.Token);
            await _localStorage.SetItemAsync("refreshToken", token.RefreshToken);
            return token;   
        }
        else
        {
            throw new Exception("Login failed");
        }
    }

    public async Task<HttpResponseMessage> Register(CreatePlayerDTO request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/Player", request);
        return response;
    }

    public async Task<T?> GetFrom<T>(string url)
    {
        await SetAuthorHeader();
        return await _httpClient.GetFromJsonAsync<T?>(url);
    }

    public HttpClient GetHttpClient() => _httpClient;
}

