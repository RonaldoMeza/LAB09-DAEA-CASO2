# Matrícula de Alumnos Implementation Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Construir el flujo MVC completo para registrar, listar y confirmar matrículas de alumnos sin persistir contraseñas.

**Architecture:** Un controlador MVC coordina vistas Razor fuertemente tipadas y un repositorio singleton, thread-safe y en memoria. El modelo concentra la validación declarativa; el servidor genera los identificadores y sanea la clave antes de almacenar datos.

**Tech Stack:** .NET 10, ASP.NET Core MVC, C#, Razor, Bootstrap 5, Data Annotations.

---

### Task 1: Modelo y repositorio

**Files:**
- Create: `Models/Alumno.cs`
- Create: `Repositories/IAlumnoRepository.cs`
- Create: `Repositories/InMemoryAlumnoRepository.cs`

1. Definir las propiedades y Data Annotations de `Alumno`.
2. Definir las operaciones de consulta, búsqueda y alta del repositorio.
3. Implementar almacenamiento sincronizado, Id generado en servidor y copia sin clave.
4. Compilar y comprobar que no existan advertencias.

### Task 2: Controlador y configuración

**Files:**
- Create: `Controllers/AlumnosController.cs`
- Modify: `Program.cs`

1. Implementar las acciones GET de listado, alta, confirmación y demo.
2. Implementar POST con antifalsificación, validación y patrón PRG.
3. Registrar el repositorio como singleton y cambiar la ruta predeterminada.
4. Compilar y comprobar rutas.

### Task 3: Interfaz Razor

**Files:**
- Create: `Views/Alumnos/Index.cshtml`
- Create: `Views/Alumnos/Create.cshtml`
- Create: `Views/Alumnos/Confirmacion.cshtml`
- Create: `Views/Alumnos/DemoHelpers.cshtml`
- Modify: `Views/Shared/_Layout.cshtml`
- Modify: `wwwroot/css/site.css`

1. Construir el formulario con todos los HTML Helpers exigidos.
2. Añadir validación visible y conservar valores ante errores.
3. Construir listado, confirmación y demo didáctica sin mostrar claves.
4. Ajustar navegación y diseño responsivo.

### Task 4: Documentación y verificación

**Files:**
- Create: `README.md`

1. Documentar MVC, repositorio, PRG, model binding y equivalencias de helpers.
2. Ejecutar `dotnet restore` y `dotnet build`.
3. Iniciar la aplicación y verificar GET, POST inválido, POST válido, confirmación e Id inexistente.
4. Inspeccionar que el HTML tenga token antifalsificación y que ninguna respuesta exponga la clave.
