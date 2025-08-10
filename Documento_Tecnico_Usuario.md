# Documento Técnico y de Usuario

## 1. Descripción General

Este proyecto es una solución .NET para la gestión de personas, basada en arquitectura limpia y N-capas. Permite registrar, consultar, actualizar y eliminar personas mediante una API web y una aplicación de consola.

---

## 2. Documento Técnico

### Validación de Datos

El proyecto utiliza **FluentValidation** para validar los datos de entrada en los DTOs antes de procesarlos. Las reglas de validación están centralizadas en la clase `PersonaValidatorRules`, lo que permite reutilizar y mantener fácilmente las reglas comunes para los distintos DTOs.

Ejemplo de reglas:

- La cédula debe tener exactamente 10 dígitos numéricos.
- El nombre es obligatorio y tiene un máximo de 50 caracteres.
- La edad debe estar entre 0 y 120 años.

La validación se aplica en los servicios antes de guardar o actualizar datos, cumpliendo con los principios SOLID y asegurando la calidad de la información.

### Arquitectura y Capas

#### 2.1 Personas.Domain

- **Propósito:** Define el núcleo del dominio, las entidades (como `Persona`) y las interfaces (como `IRepository`).
- **Responsabilidad:** No depende de otras capas. Es el corazón del sistema y contiene la lógica de negocio más pura.
- **Ejemplo:**
  - Entidad: `Persona` (Id, Cedula, Nombres, Edad, Direccion, Estado)
  - Interface: `IRepository<T>` para operaciones CRUD genéricas.

#### 2.2 Personas.Application

- **Propósito:** Implementa la lógica de negocio y los servicios que usan las entidades e interfaces del dominio.
- **Responsabilidad:** Depende solo de Domain. Aquí se encuentran los servicios (`PersonaService`), DTOs y mapeos.
- **Ejemplo:**
  - Servicio: `PersonaService` para gestionar personas.
  - DTOs: `PersonaDto`, `PersonaCreateDto`, `PersonaUpdateDto`.

#### 2.3 Personas.Infrastructure

- **Propósito:** Implementa el acceso a datos y la persistencia usando Entity Framework.
- **Responsabilidad:** Depende de Domain. Implementa los repositorios y el DbContext.
- **Ejemplo:**
  - Repositorio: `Repository<T>` implementa `IRepository<T>`.
  - DbContext: `AppDbContext` para la conexión a la base de datos.

#### 2.4 Personas.ApiWebApplication

- **Propósito:** Expone la lógica de negocio como una API web.
- **Responsabilidad:** Depende de Application y Domain. Recibe peticiones HTTP y responde con datos.
- **Ejemplo:**
  - Controlador: `PersonasController` para endpoints CRUD.

#### 2.5 Personas.ConsoleApp

- **Propósito:** Permite interactuar con la lógica de negocio desde la consola.
- **Responsabilidad:** Depende de Application y Domain. Útil para pruebas o administración.

---

## 3. Documento de Usuario

### ¿Qué hace cada capa?

- **Domain:** Define qué es una persona y cómo se representa en el sistema.
- **Application:** Gestiona las reglas de negocio y transforma datos entre entidades y DTOs.
- **Infrastructure:** Guarda y recupera personas en la base de datos.
- **ApiWebApplication:** Permite interactuar con el sistema desde aplicaciones externas (por ejemplo, un frontend o Postman).
- **ConsoleApp:** Permite interactuar desde la línea de comandos.

### ¿Cómo usar el sistema?

1. **Instalación:**
   - Clona el repositorio y abre la solución en Visual Studio.
   - Restaura paquetes NuGet y configura la base de datos.
2. **Ejecución:**
   - Para la API, ejecuta `Personas.ApiWebApplication`.
   - Para la consola, ejecuta `Personas.ConsoleApp`.
3. **Interacción:**
   - Usa la API para crear, consultar, actualizar y eliminar personas.
   - Prueba los endpoints con Postman, Swagger o desde la consola.

### Ejemplo de Flujo

- El usuario envía una petición POST a la API para crear una persona.
- El controlador recibe la petición y llama al servicio de aplicación.
- El servicio transforma el DTO en entidad y usa el repositorio para guardar la persona.
- El repositorio accede a la base de datos y persiste la entidad.
- La respuesta se envía al usuario.

---

## 4. Diagrama de Dependencias

```
[ApiWebApplication]   [ConsoleApp]
        |                  |
        v                  v
   [Application] <------> [Domain]
        |
        v
 [Infrastructure]
```

- Las flechas indican la dirección de las dependencias.
- El núcleo es Domain; las demás capas dependen de él, nunca al revés.

---

## 5. Contacto y Soporte

Para dudas, sugerencias o soporte, contacta al autor del repositorio.
