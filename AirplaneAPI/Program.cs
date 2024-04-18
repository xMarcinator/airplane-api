using AirplaneAPI.Database;
using AirplaneAPI.Database.Repositories.Implementation;
using AirplaneAPI.Database.Repositories.Interface;
using AirplaneAPI.utils;
using dotenv.net;
using Microsoft.EntityFrameworkCore;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IFlightRepo,FlightRepo>();
builder.Services.AddScoped<IAirplaneRepo,AirplaneRepo>();
builder.Services.AddScoped<IAirportRepo,AirportRepo>();

var app = builder.Build();


var seedingTask =  app.SeedContexts(builder.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

app.MapControllerRoute(name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseHttpsRedirection();

Task.WaitAll(seedingTask);

app.Run();

public partial class Program
{
}