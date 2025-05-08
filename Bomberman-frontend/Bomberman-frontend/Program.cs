using Bomberman_frontend.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using BombermanGame.Source.Engine.Input;
using MudBlazor.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PlayerInput>();
builder.Services.AddHttpClient();
builder.Services.AddMudServices();
builder.Services.AddHostedService<MqttClientService>();
builder.Services.AddSingleton<MqttClientService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
