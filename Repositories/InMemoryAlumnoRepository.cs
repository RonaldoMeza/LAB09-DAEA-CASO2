using CASO2_LAB09.Models;

namespace CASO2_LAB09.Repositories;

public sealed class InMemoryAlumnoRepository : IAlumnoRepository
{
    private readonly List<Alumno> _alumnos = [];
    private readonly Lock _bloqueo = new();
    private int _siguienteId = 1;

    public IReadOnlyList<Alumno> ObtenerTodos()
    {
        lock (_bloqueo)
        {
            return _alumnos.Select(Copiar).ToList().AsReadOnly();
        }
    }

    public Alumno? ObtenerPorId(int id)
    {
        lock (_bloqueo)
        {
            var alumno = _alumnos.FirstOrDefault(item => item.Id == id);
            return alumno is null ? null : Copiar(alumno);
        }
    }

    public Alumno Agregar(Alumno alumno)
    {
        ArgumentNullException.ThrowIfNull(alumno);

        lock (_bloqueo)
        {
            // El Id enviado por el navegador se ignora y la clave nunca entra al repositorio.
            var alumnoGuardado = new Alumno
            {
                Id = _siguienteId++,
                Nombres = alumno.Nombres.Trim(),
                Correo = alumno.Correo.Trim(),
                Clave = string.Empty,
                Carrera = alumno.Carrera,
                Turno = alumno.Turno,
                Becado = alumno.Becado,
                Observaciones = alumno.Observaciones?.Trim() ?? string.Empty
            };

            _alumnos.Add(alumnoGuardado);
            return Copiar(alumnoGuardado);
        }
    }

    private static Alumno Copiar(Alumno alumno) => new()
    {
        Id = alumno.Id,
        Nombres = alumno.Nombres,
        Correo = alumno.Correo,
        Clave = string.Empty,
        Carrera = alumno.Carrera,
        Turno = alumno.Turno,
        Becado = alumno.Becado,
        Observaciones = alumno.Observaciones
    };
}
