using System.Collections.Concurrent;
using System.Text.Json.Nodes;
using CAN2JSON;
using CAN2JSON.BackgroundServices;
using CAN2JSON.Data.Context;
using CAN2JSON.Data.Logic;
using CAN2JSON.Data.Repository;
using CAN2JSON.Interfaces;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<Can2JsonContext>
    (options => options.UseSqlite("Name=BMSStats"));

builder.Services.AddHostedService<SerialDataBackgroundService>();
builder.Services.AddHostedService<DbBackgroundService>();
builder.Services.AddHostedService<InfluxDbBackgroundService>();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBmsLogic, BmsLogic>();
builder.Services.AddScoped<IBatteryLogic, BatteryLogic>();
builder.Services.AddSingleton<ApplicationInstance>();
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public class ApplicationInstance {

    public ConcurrentDictionary<string, object> Application { get; } = new ConcurrentDictionary<string, object>();
}