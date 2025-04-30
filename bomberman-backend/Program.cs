using System.Diagnostics;
using System.Text;
using bomberman_backend.Data;
using bomberman_backend.Repository;
using bomberman_backend.Repository.Interfaces;
using bomberman_backend.Services;
using bomberman_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContextcs>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("dbcontext"));
});

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://bomberman-jps.onrender.com/",
                                              "https://ep-old-field-a90f0ba8-pooler.gwc.azure.neon.tech")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

// Repos
builder.Services.AddScoped<IUserRepo, UserRepo>();

//Services
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/healthz");

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);

app.Run();
