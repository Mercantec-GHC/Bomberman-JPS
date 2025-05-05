using Bomberman_frontend.Client.Pages;
using Bomberman_frontend.Components;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddSingleton<MqttClientService>();
builder.Services.AddMudServices();
var mqttService = new MqttClientService();
var cts = new CancellationTokenSource();

await mqttService.ConnectAsync(cts.Token);


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Bomberman_frontend.Client._Imports).Assembly);

app.Run();
