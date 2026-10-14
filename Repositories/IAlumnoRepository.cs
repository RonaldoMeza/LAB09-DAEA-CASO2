using CASO2_LAB09.Models;

namespace CASO2_LAB09.Repositories;

public interface IAlumnoRepository
{
    IReadOnlyList<Alumno> ObtenerTodos();
    Alumno? ObtenerPorId(int id);
    Alumno Agregar(Alumno alumno);
}
