using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models;
using RestaurantApi.Models.Dtos;
using RestaurantApi.Services;
using RestaurantApi.Services.IRestaurant;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EtteremContext>(
    option =>
    {
        var ConnectionString = builder.Configuration.GetConnectionString("MySQL");
        option.UseMySQL(ConnectionString);
    }
    
    
    );




builder.Services.AddScoped<IRendeles, RendelesService>();
builder.Services.AddScoped<ITermek,TermekService>();
builder.Services.AddScoped<resultResponseDto>();
builder.Services.AddScoped<IKapcsolo, KapcsoloService>();

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
