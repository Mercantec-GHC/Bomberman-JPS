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

public class LayoutService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public LayoutService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public event Action? OnLayoutChanged;

    public void NotifyLayoutChanged()
    {
        OnLayoutChanged?.Invoke();
    }
}