using System.Collections.Concurrent;
using System.Text.Json.Nodes;
using CAN2JSON;
using CAN2JSON.Data.Context;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHostedService<SerialDataBackgroundService>();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ApplicationInstance>();
builder.Services.AddDbContext<Can2JsonContext>
    (options => options.UseSqlite("Name=BMSStats"));

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