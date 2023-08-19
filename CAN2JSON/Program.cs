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
var sqlite = builder.Configuration.GetValue<string>("UseSqLite");
if (sqlite is "True")
{
    builder.Services.AddDbContext<Can2JsonContext>
        (options => options.UseSqlite("Name=BMSStats"));
    builder.Services.AddHostedService<SqliteDbBackgroundService>();
    /* These are for when we'd like to do more CRUD related actions with our battery readings...
     * builder.Services.AddScoped<IBmsLogic, BmsLogic>();
     * builder.Services.AddScoped<IBatteryLogic, BatteryLogic>();
     */
}


// Background workers 
builder.Services.AddHostedService<SerialDataBackgroundService>();
builder.Services.AddHostedService<InfluxDbBackgroundService>();

// Optional RS485
var batteryRs485 = builder.Configuration.GetValue<string>("BatterySerial:RS485");
if(batteryRs485 is "True") builder.Services.AddHostedService<Rs485BackgroundService>();
builder.Services.AddSwaggerGen();



// This is our concurrent dictionary for accessing our latest serial readings from our workers
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

// app.UseAuthorization();

app.MapControllers();

app.Run();

public class ApplicationInstance {

    public ConcurrentDictionary<string, object> Application { get; } = new();
}