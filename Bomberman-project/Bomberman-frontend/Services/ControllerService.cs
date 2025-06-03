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

public class ControllerService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public ControllerService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task ChangeLEDBrightness(UpdateControllerLEDBrightnessDTO dto)
    {
        var response = await _httpClient.PutAsJsonAsync<UpdateControllerLEDBrightnessDTO>("api/Carrier/controller/led", dto);
    }

    public async Task ChangeColor(UpdateControllerPlayerColorDTO dto)
    {
        var response = await _httpClient.PutAsJsonAsync<UpdateControllerPlayerColorDTO>("api/Carrier/controller/playercolor", dto);
    }
}