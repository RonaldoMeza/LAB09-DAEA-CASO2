# Implementación del Caso 2 — ASP.NET Core MVC

Actúa como un desarrollador senior especializado en ASP.NET Core MVC, C#, Razor y buenas prácticas de programación.

## Contexto

Estoy desarrollando el proyecto académico `CASO2-LAB09`, correspondiente a un sistema web de matrícula de alumnos para un instituto.

El proyecto ya está creado con la plantilla ASP.NET Core MVC. Necesito implementar el caso completo, manteniendo una arquitectura MVC correcta, código limpio y fácil de explicar durante una exposición académica.

**Importante:** No crees otro proyecto. Trabaja sobre la estructura existente y verifica primero el framework configurado en el archivo `.csproj`.

## 1. Arquitectura y buenas prácticas

- Respetar estrictamente el patrón MVC.
- Utilizar C# y vistas Razor fuertemente tipadas.
- Mantener la lógica de presentación en las vistas y la coordinación de solicitudes en los controladores.
- Crear `Alumno.cs` en Models.
- Crear `AlumnosController.cs` en Controllers.
- Implementar un repositorio sencillo en memoria mediante `IAlumnoRepository` e `InMemoryAlumnoRepository`.
- Registrar el repositorio con inyección de dependencias como Singleton.
- Aplicar nombres descriptivos y convenciones de C#.
- Agregar comentarios breves únicamente en partes importantes para la exposición.
- No utilizar Entity Framework, SQL Server, API externas ni paquetes innecesarios.

## 2. Modelo Alumno

Crear las propiedades:

- Id: int
- Nombres: string
- Correo: string
- Clave: string
- Carrera: string
- Turno: string
- Becado: bool
- Observaciones: string

Implementar validaciones con Data Annotations, incluyendo Required, EmailAddress, StringLength y Display según corresponda.

Evitar advertencias de nulabilidad y utilizar validación del lado del servidor.

La clave debe utilizarse únicamente para demostrar `PasswordFor`; nunca almacenarla en texto plano, mostrarla en listados ni incluirla en la confirmación.

## 3. Controlador

Implementar:

**GET /Alumnos/Index**
- Mostrar alumnos registrados.

**GET /Alumnos/Create**
- Mostrar el formulario vacío.
- Preparar la lista de carreras usando `SelectList`.
- Enviar los datos necesarios a la vista.

**POST /Alumnos/Create**
- Recibir el objeto Alumno mediante model binding.
- Implementar `[HttpPost]` y `[ValidateAntiForgeryToken]`.
- Verificar `ModelState.IsValid`.
- Si hay errores, volver a mostrar el formulario, reconstruyendo la lista de carreras y conservando los valores válidos.
- Si es válido, asignar un Id desde el servidor y registrar al alumno sin guardar su contraseña.
- Redirigir a Confirmacion aplicando el patrón Post/Redirect/Get.

**GET /Alumnos/Confirmacion/{id}**
- Recuperar al alumno registrado.
- Mostrar su información.
- Manejar correctamente identificadores inexistentes.

## 4. Formulario Create.cshtml

Utilizar obligatoriamente los siguientes HTML Helpers:

- `Html.BeginForm`
- `Html.LabelFor`
- `Html.TextBoxFor`
- `Html.PasswordFor`
- `Html.TextAreaFor`
- `Html.DropDownListFor`
- `Html.RadioButtonFor`
- `Html.CheckBoxFor`
- `Html.HiddenFor`

La vista debe declarar `@model Alumno`, utilizando el namespace correspondiente.

Diseñar un formulario profesional con Bootstrap, responsivo, ordenado y visualmente atractivo.

Campos:

- Id: HiddenFor, solo como demostración; nunca confiar en el valor enviado por el cliente.
- Nombres: TextBoxFor.
- Correo: TextBoxFor.
- Clave: PasswordFor.
- Carrera: DropDownListFor.
- Turno: RadioButtonFor (Mañana, Tarde, Noche).
- Becado: CheckBoxFor.
- Observaciones: TextAreaFor.

Incluir mensajes de validación y resumen de errores.

No reemplazar los HTML Helpers exigidos con Tag Helpers como `asp-for`.

## 5. Vista Confirmacion.cshtml

- Declarar `@model Alumno`.
- Mostrar mensaje de matrícula exitosa.
- Utilizar `Html.DisplayFor` para presentar los datos registrados.
- No mostrar claves.
- Agregar enlaces para crear otra matrícula o regresar al listado.

## 6. Vista Index.cshtml

Crear una tabla Bootstrap que muestre:

- Id
- Nombres
- Correo
- Carrera
- Turno
- Becado

Agregar botón para registrar nuevos alumnos y un estado vacío cuando todavía no existan registros.

## 7. Demostración de HTML Helpers

Incluir una sección didáctica o una vista adicional denominada `DemoHelpers.cshtml`, accesible desde la aplicación.

Comparar:

`@Html.TextBox("Nombres")`

frente a:

`@Html.TextBoxFor(m => m.Nombres)`

Explicar brevemente cómo generan los atributos `id` y `name`, y por qué los helpers fuertemente tipados son más seguros frente a errores de refactorización.

La demostración debe permitir inspeccionar el HTML con las herramientas de desarrollo del navegador.

## 8. Configuración y navegación

- Revisar Program.cs y mantener el middleware MVC correctamente configurado.
- Configurar la ruta inicial hacia el listado o formulario de alumnos.
- Mantener Bootstrap disponible y un diseño consistente.
- No eliminar archivos existentes sin necesidad.

## 9. Validación técnica

Al terminar:

1. Ejecutar `dotnet restore`.
2. Ejecutar `dotnet build`.
3. Corregir errores de compilación.
4. Ejecutar la aplicación si el entorno lo permite.
5. Verificar navegación GET y POST.
6. Verificar validaciones, opciones de carrera, turnos y checkbox.
7. Comprobar el registro en memoria y la confirmación.
8. Verificar que no se almacena ni se muestra la clave.
9. Verificar la protección antifalsificación y que el Id no se acepta como dato confiable del cliente.

No afirmar que una prueba pasó si no pudo ejecutarse.

## 10. Documentación para exposición

Crear un archivo `README.md` que contenga:

- Objetivo del sistema.
- Estructura del proyecto.
- Explicación de Model, View y Controller.
- Responsabilidad del repositorio en memoria.
- Flujo GET → formulario → POST → validación → confirmación.
- Tabla de equivalencias HTML Helper → HTML generado.
- Diferencias entre TextBox y TextBoxFor.
- Explicación de model binding.
- Uso y riesgo de HiddenFor.
- Instrucciones para ejecutar y demostrar la aplicación.

El README debe ser conciso, comprensible y útil para estudiar la exposición.

## Resultado esperado

Entregar una aplicación ASP.NET Core MVC funcional, organizada y visualmente presentable, cumpliendo todos los requisitos del Caso 2.

Priorizar buenas prácticas sin implementar una arquitectura excesivamente compleja.

**Antes de programar:** inspeccionar el proyecto "CASO2-LAB09" existente y presentar un plan breve de los archivos a crear o modificar. Después, implementar, compilar y verificar la solución.


**Instrucciones adicionales del entorno:**

Estoy trabajando en Visual Studio Code con .NET SDK 10.0.401 y ASP.NET Core MVC (.NET 10).

La ubicación del proyecto es:

`F:\DAEA\LAB09-DAEA\CASO2-LAB09`

Ya ejecuté `dotnet restore` y `dotnet build` exitosamente.

Trabaja únicamente dentro de este proyecto. No crees otra solución ni cambies la versión del framework.

Mantén el código organizado y fácil de explicar, porque debo realizar una exposición académica mostrando el funcionamiento y el código fuente.

Al finalizar, indícame qué archivos modificaste, cómo ejecutar la aplicación y qué partes del código debo mostrar durante la exposición.