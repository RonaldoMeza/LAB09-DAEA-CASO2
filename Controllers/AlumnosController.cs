using CASO2_LAB09.Models;
using CASO2_LAB09.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASO2_LAB09.Controllers;

public class AlumnosController(IAlumnoRepository repository) : Controller
{
    private static readonly string[] Carreras =
    [
        "Administración de Empresas",
        "Contabilidad",
        "Desarrollo de Sistemas de Información",
        "Diseño Gráfico",
        "Enfermería Técnica"
    ];

    [HttpGet]
    public IActionResult Index() => View(repository.ObtenerTodos());

    [HttpGet]
    public IActionResult Create()
    {
        PrepararCarreras();
        return View(new Alumno());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Id,Nombres,Correo,Clave,Carrera,Turno,Becado,Observaciones")] Alumno alumno)
    {
        ValidarOpciones(alumno);

        if (!ModelState.IsValid)
        {
            PrepararCarreras(alumno.Carrera);
            return View(alumno);
        }

        var alumnoRegistrado = repository.Agregar(alumno);
        return RedirectToAction(nameof(Confirmacion), new { id = alumnoRegistrado.Id });
    }

    [HttpGet]
    public IActionResult Confirmacion(int id)
    {
        var alumno = repository.ObtenerPorId(id);
        return alumno is null ? NotFound() : View(alumno);
    }

    [HttpGet]
    public IActionResult DemoHelpers() => View(new Alumno());

    private void PrepararCarreras(string? seleccionada = null)
    {
        ViewBag.Carreras = new SelectList(Carreras, seleccionada);
    }

    private void ValidarOpciones(Alumno alumno)
    {
        if (!string.IsNullOrWhiteSpace(alumno.Carrera) && !Carreras.Contains(alumno.Carrera))
        {
            ModelState.AddModelError(nameof(Alumno.Carrera), "Seleccione una carrera válida.");
        }

        string[] turnosValidos = ["Mañana", "Tarde", "Noche"];
        if (!string.IsNullOrWhiteSpace(alumno.Turno) && !turnosValidos.Contains(alumno.Turno))
        {
            ModelState.AddModelError(nameof(Alumno.Turno), "Seleccione un turno válido.");
        }
    }
}
