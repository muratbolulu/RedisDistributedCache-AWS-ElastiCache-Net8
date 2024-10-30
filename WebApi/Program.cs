using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using WebApi.Services.Abstract;
using WebApi.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


#region redis distributed cache (AWS ElastiCache)
var redisConnectionString = builder.Configuration.GetSection("Redis:ConnectionString").Value;

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));

builder.Services.AddSingleton<IRedisCacheService, RedisCacheService>();
#endregion


builder.Services.AddControllers();
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
