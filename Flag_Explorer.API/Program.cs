

using Flag_Explorer.Domain.DTO;
using Flag_Explorer.Repository.Contracts;
using Flag_Explorer.Repository.Modules;
using Flag_Explorer.UseCase;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

// Start Repositories
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
// End Repositories

//Start UseCase
builder.Services.AddScoped<GetAllCountriesUseCase>();
builder.Services.AddScoped<GetByNameCountryUseCase>();
//End UseCase
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty; // Opens Swagger at root URL
    });

}




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
