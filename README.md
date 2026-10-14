# CASO2-LAB09 — Matrícula de alumnos

Aplicación académica ASP.NET Core MVC para registrar alumnos, validar sus datos, listarlos y mostrar una confirmación. Usa un repositorio en memoria; no utiliza base de datos ni almacena la clave ingresada.

## Estructura y responsabilidades

- `Models/Alumno.cs`: representa los datos y declara validaciones con Data Annotations.
- `Views/Alumnos`: interfaz Razor tipada para listado, formulario, confirmación y demostración.
- `Controllers/AlumnosController.cs`: coordina solicitudes, valida opciones, reconstruye listas y decide la respuesta.
- `Repositories`: abstrae y mantiene los registros durante la vida del proceso. Al reiniciar la aplicación, los datos se pierden.
- `Program.cs`: registra MVC, el repositorio singleton y la ruta inicial.

En MVC, el **Model** describe y valida la información; la **View** presenta HTML; el **Controller** recibe la solicitud y coordina el flujo. El repositorio separa el acceso a datos del controlador y genera el Id bajo control del servidor.

## Flujo de matrícula

1. `GET /Alumnos/Create` prepara un `SelectList` y presenta el formulario vacío.
2. El navegador envía `POST /Alumnos/Create`; model binding construye un `Alumno` desde los campos cuyos atributos `name` coinciden con sus propiedades.
3. Data Annotations y las validaciones de opciones alimentan `ModelState`.
4. Si hay errores, se reconstruyen las carreras y se devuelve la misma vista con los valores válidos.
5. Si todo es válido, el repositorio ignora el Id del cliente, asigna uno nuevo y guarda una copia cuya `Clave` está vacía.
6. El controlador redirige a `GET /Alumnos/Confirmacion/{id}` (patrón Post/Redirect/Get), evitando repetir el POST al recargar.

## HTML Helpers

| Helper Razor | HTML generado (simplificado) |
|---|---|
| `LabelFor(m => m.Nombres)` | `<label for="Nombres">Nombres completos</label>` |
| `TextBoxFor(m => m.Nombres)` | `<input id="Nombres" name="Nombres" type="text">` |
| `PasswordFor(m => m.Clave)` | `<input id="Clave" name="Clave" type="password">` |
| `TextAreaFor(m => m.Observaciones)` | `<textarea id="Observaciones" name="Observaciones"></textarea>` |
| `DropDownListFor(m => m.Carrera, ...)` | `<select id="Carrera" name="Carrera">...</select>` |
| `RadioButtonFor(m => m.Turno, "Mañana")` | `<input name="Turno" type="radio" value="Mañana">` |
| `CheckBoxFor(m => m.Becado)` | checkbox más un campo oculto para enviar `false` |
| `HiddenFor(m => m.Id)` | `<input id="Id" name="Id" type="hidden">` |

`TextBox("Nombres")` usa una cadena: compila incluso si la propiedad cambia. `TextBoxFor(m => m.Nombres)` usa una expresión tipada, aprovecha metadatos del modelo y permite que el compilador detecte errores de refactorización. La página `/Alumnos/DemoHelpers` permite inspeccionar ambos resultados.

### Model binding y HiddenFor

Model binding relaciona los pares nombre/valor enviados por el formulario con las propiedades del parámetro `Alumno`. Un campo oculto no es seguro: el usuario puede modificarlo desde el navegador. Por eso `Id` aparece como demostración, pero `InMemoryAlumnoRepository` siempre lo reemplaza por una secuencia generada en el servidor.

El formulario incluye un token antifalsificación. El atributo `[ValidateAntiForgeryToken]` rechaza publicaciones que no tengan un token válido.

## Ejecución y demostración

Desde `CASO2-LAB09`:

```powershell
dotnet restore
dotnet build
dotnet run --launch-profile http
```

Abrir `http://localhost:5010`. Para la exposición:

1. Mostrar `Alumno.cs` y sus Data Annotations.
2. Recorrer GET y POST de `AlumnosController`, destacando `ModelState`, `SelectList` y PRG.
3. Mostrar cómo el repositorio asigna el Id y establece `Clave = string.Empty`.
4. Inspeccionar `Create.cshtml`: helpers, antifalsificación y mensajes de validación.
5. Enviar datos incompletos, luego una matrícula válida y revisar listado/confirmación.
6. Abrir Demo Helpers e inspeccionar `id` y `name` en las herramientas del navegador.
