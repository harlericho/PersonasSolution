﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Personas.Application.DTOs;
using Personas.Application.Mappings;
using Personas.Application.Services;
using Personas.Application.Validators;
using Personas.Domain.Interfaces;
using Personas.Infrastructure.Data;
using Personas.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);


// Define una política de CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Registrar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins(
                "http://127.0.0.1:5500",  // Live Server
                "http://localhost:5500"   // por si usas localhost
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});


// FluentValidation
// Registrar validaciones automáticas en backend
builder.Services.AddFluentValidationAutoValidation();

// Registrar validaciones para adaptadores cliente (ej. jQuery Validation)
builder.Services.AddFluentValidationClientsideAdapters();

// Registrar tu validador explícitamente (opcional si usas RegisterValidatorsFromAssembly)
builder.Services.AddScoped<IValidator<PersonaCreateDto>, PersonaCreateValidator>();
builder.Services.AddScoped<IValidator<PersonaUpdateDto>, PersonaUpdateValidator>();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependencias
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<PersonaService>();

// AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(PersonaProfile));




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

// Aquí activas CORS
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
