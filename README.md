# PersonasSolution

Proyecto .NET con arquitectura limpia y N-capas para la gestión de personas.

## Validación de Datos

El proyecto utiliza **FluentValidation** para validar los datos de entrada en los DTOs antes de procesarlos. Las reglas de validación están centralizadas en la clase `PersonaValidatorRules`, lo que permite reutilizar y mantener fácilmente las reglas comunes para los distintos DTOs.

Ejemplo de reglas:

- La cédula debe tener exactamente 10 dígitos numéricos.
- El nombre es obligatorio y tiene un máximo de 50 caracteres.
- La edad debe estar entre 0 y 120 años.

La validación se aplica en los servicios antes de guardar o actualizar datos, cumpliendo con los principios SOLID y asegurando la calidad de la información.

## Estructura del Proyecto

- **Personas.Domain**: Núcleo del dominio. Contiene entidades y interfaces.
- **Personas.Application**: Lógica de negocio, servicios, DTOs y mapeos.
- **Personas.Infrastructure**: Acceso a datos, implementación de repositorios y DbContext.
- **Personas.ApiWebApplication**: API Web para exponer los servicios.
- **Personas.ConsoleApp**: Aplicación de consola para pruebas o administración.

## Arquitectura

El proyecto sigue los principios de arquitectura limpia y SOLID:

- Separación de responsabilidades por capas.
- Las dependencias fluyen hacia el dominio.
- Uso de interfaces y abstracciones.
- Inyección de dependencias.

## Principios SOLID

- **S**: Cada clase tiene una responsabilidad única.
- **O**: El código es extensible sin modificar el existente.
- **L**: Las implementaciones pueden ser sustituidas por sus abstracciones.
- **I**: Las interfaces son específicas y no obligan a implementar métodos innecesarios.
- **D**: Las dependencias se inyectan y dependen de abstracciones.

## Instalación

1. Clona el repositorio:
   ```
   git clone <url-del-repositorio>
   ```
2. Abre la solución `PersonasSolution.sln` en Visual Studio.
3. Restaura los paquetes NuGet.
4. Configura la cadena de conexión en `Personas.ApiWebApplication/appsettings.json` y/o `Personas.Infrastructure/Data/AppDbContext.cs`.
5. Ejecuta migraciones si usas Entity Framework:
   ```
   dotnet ef database update --project Personas.Infrastructure
   ```

## Ejecución

- Para iniciar la API Web:
  1. Establece `Personas.ApiWebApplication` como proyecto de inicio.
  2. Ejecuta (F5 o Ctrl+F5).
- Para usar la consola:
  1. Establece `Personas.ConsoleApp` como proyecto de inicio.
  2. Ejecuta.

## Uso

La API expone endpoints para CRUD de personas. Puedes probarlos con herramientas como Postman o Swagger.

## Ejemplo de Estructura de Carpetas

```
PersonasSolution.sln
Personas.ApiWebApplication/
Personas.Application/
Personas.Domain/
Personas.Infrastructure/
Personas.ConsoleApp/
```

## Contribución

1. Haz un fork del repositorio.
2. Crea una rama para tu feature/fix.
3. Haz tus cambios y crea un pull request.

## Licencia

Este proyecto está bajo la licencia MIT.

## Contacto

Para dudas o soporte, contacta al autor del repositorio.
