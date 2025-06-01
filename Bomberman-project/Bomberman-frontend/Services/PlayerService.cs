using System.Security.Claims;
using Blazored.LocalStorage;
using DomainModels;
using DomainModels.DTO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Session;

public class PlayerService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public PlayerService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<Player> GetPlayer(string id)
    {
        var response = await _httpClient.GetFromJsonAsync<Player>($"/api/Player?id={id}");
        if (response is Player player)
        {
            return player;
        }
        else
        {
            throw new Exception("Player not found!");
        }
    }
}