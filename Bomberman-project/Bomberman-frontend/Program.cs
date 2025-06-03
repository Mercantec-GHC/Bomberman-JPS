using Bomberman_frontend.Components;
using DomainModels;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PlayerInput>();
builder.Services.AddHttpClient();
builder.Services.AddMudServices();


// Authentication and authorization
IConfiguration configuration = builder.Configuration;
var baseUrl = configuration["BaseUrl"] ??
                Environment.GetEnvironmentVariable("BaseUrl");
builder.Services.AddHttpClient<AuthService>("api", options =>
{
    options.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddHttpClient<PlayerService>("api", options =>
{
    options.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddHttpClient<UserService>("api", options =>
{
    options.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddHttpClient<ControllerService>("api", options =>
{
    options.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddScoped<LayoutService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthProvider>();
builder.Services.AddAuthenticationCore();
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddBlazoredLocalStorage();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();




app.UseExceptionHandler("/Error", createScopeForErrors: true);
// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHsts();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();