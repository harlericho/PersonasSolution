// See https://aka.ms/new-console-template for more information
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Personas.Application.DTOs;
using Personas.Application.Mappings;
using Personas.Application.Services;
using Personas.Application.Validators;
using Personas.Domain.Interfaces;
using Personas.Infrastructure.Data;
using Personas.Infrastructure.Repositories;

//Console.WriteLine("Hello, World!");
class Program
{
    static async Task Main()
    {
        var services = new ServiceCollection();
        // Registrar logging
        services.AddLogging(builder =>
        {
            builder.AddConsole(); // Para que también imprima en consola
        });

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Server=HARLERICHO-PC\\SQLEXPRESS;Database=GestorTareasDB;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=true"));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<PersonaService>();
        services.AddAutoMapper(cfg => { }, typeof(PersonaProfile).Assembly);

        // Aquí registrar validadores si no está registrado aún:
        services.AddScoped<IValidator<PersonaCreateDto>, PersonaCreateValidator>();
        services.AddScoped<IValidator<PersonaUpdateDto>, PersonaUpdateValidator>();


        var provider = services.BuildServiceProvider();
        var service = provider.GetRequiredService<PersonaService>();

        Console.WriteLine("Lista de Personas:");
        var personas = await service.GetAllPersonasAsync();
        foreach (var p in personas)
        {
            Console.WriteLine($"{p.Id} - {p.Nombres} ({p.Cedula})");
        }
        Console.WriteLine("\nAgregando una nueva persona...");
        await service.AddPersonaAsync(new PersonaCreateDto
        {
            Cedula = "1234567890",
            Nombres = "Juan Perez",
            Edad = 30,
            Direccion = "Calle Falsa 123"
        });
        Console.WriteLine("Persona agregada. Lista actualizada:");
        personas = await service.GetAllPersonasAsync();
        foreach (var p in personas)
        {
            Console.WriteLine($"{p.Id} - {p.Nombres} ({p.Cedula})");
        }
        Console.WriteLine("\nActualizando la persona agregada...");
        var personaToUpdate = personas.FirstOrDefault(p => p.Cedula == "1234567890");

        if (personaToUpdate != null)
        {
            await service.UpdatePersonaAsync(new PersonaUpdateDto
            {
                Id = personaToUpdate.Id,
                Cedula = personaToUpdate.Cedula,
                Nombres = "Juan Perez Actualizado",
                Edad = 31,
                Direccion = "Calle Falsa 123 Actualizada"
            });
            Console.WriteLine("Persona actualizada. Lista actualizada:");
            personas = await service.GetAllPersonasAsync();
            foreach (var p in personas)
            {
                Console.WriteLine($"{p.Id} - {p.Nombres} ({p.Cedula})");
            }
        }
        else
        {
            Console.WriteLine("No se encontró la persona para actualizar.");
        }
        //Console.WriteLine("\nEliminando la persona agregada...");
        //if (personaToUpdate != null)
        //{
        //    await service.DeletePersonaAsync(personaToUpdate.Id);
        //    Console.WriteLine("Persona eliminada. Lista actualizada:");
        //    personas = await service.GetAllPersonasAsync();
        //    foreach (var p in personas)
        //    {
        //        Console.WriteLine($"{p.Id} - {p.Nombres} ({p.Cedula})");
        //    }
        //}
        //else
        //{
        //    Console.WriteLine("No se encontró la persona para eliminar.");
        //}
        Console.WriteLine("Presione cualquier tecla para salir...");
        Console.ReadKey();

    }
}