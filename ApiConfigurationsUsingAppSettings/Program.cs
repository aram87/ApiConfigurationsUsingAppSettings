using ApiConfigurationsUsingAppSettings.Filters;
using ApiConfigurationsUsingAppSettings.Interfaces;
using ApiConfigurationsUsingAppSettings.Middleware;
using ApiConfigurationsUsingAppSettings.Models;
using ApiConfigurationsUsingAppSettings.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.OperationFilter<ApiKeyHeaderOperationFilter>());
builder.Services.AddTransient<IHashingService, HashingService>();

var section = builder.Configuration.GetSection("ApiSettings");
builder.Services.Configure<ApiSettingsModel>(section);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ApiKeyMiddleware>();
app.MapControllers();

app.Run();