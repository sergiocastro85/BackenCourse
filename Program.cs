using WebApplicationBackend.Models;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using WebApplicationBackend.DTOs;
using WebApplicationBackend.Interface;
using WebApplicationBackend.Validators;
using WebApplicationBackend.Repository;
using WebApplicationBackend.AutoMapers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Entity Framework
builder.Services.AddDbContext<StoreContext>(opitions =>
{
    opitions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//FluentValidation
builder.Services.AddScoped<IValidator<BeerInsertDto>, BeerInsertValidator>();
builder.Services.AddScoped<IValidator<BeerUpdateDto>, BeerUpdateValidator>();

//Mapper   

builder.Services.AddAutoMapper(typeof(MapingProfile));

//Repository
builder.Services.AddScoped<IRepository<Beer>, BeerRepository>();

//interface BeerService
builder.Services.AddKeyedScoped<ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>, BeerService>("beerService");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddControllers();




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
