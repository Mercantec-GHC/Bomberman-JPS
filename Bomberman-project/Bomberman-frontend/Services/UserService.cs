using System.Security.Claims;
using Blazored.LocalStorage;
using DomainModels;
using DomainModels.DTO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Session;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;

public class UserService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public UserService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<UpdateUserInfoDTO> ChangeInfo(Guid id, UpdateUserInfoDTO dto)
    {
        var response = await _httpClient.PutAsJsonAsync<UpdateUserInfoDTO>($"/api/Users/info?id={id}", dto);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<UpdateUserInfoDTO>();
            return result!;
        }
        else
        {
            throw new Exception("Player not found!");
        }
    }

    public async Task<UpdateUserPasswordDTO> ChangePassword(Guid id, UpdateUserPasswordDTO dto)
    {
        var response = await _httpClient.PutAsJsonAsync<UpdateUserPasswordDTO>($"/api/Users/password?id={id}", dto);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<UpdateUserPasswordDTO>();
            return result!;
        }
        else
        {
            throw new Exception("Player not found!");
        }
    }

    public async Task CallRefreshToken()
    {
        var tokenToBeUsed = await _localStorage.GetItemAsync<TokenRequest>("refreshToken");
        await _localStorage.RemoveItemAsync("refreshToken");
        await _localStorage.RemoveItemAsync("authToken");
        var response = await _httpClient.PostAsJsonAsync($"/api/Player/refreshToken", tokenToBeUsed);
        Console.WriteLine(response.Content.ReadAsStringAsync());
    }
}