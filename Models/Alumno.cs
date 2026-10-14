using System.ComponentModel.DataAnnotations;

namespace CASO2_LAB09.Models;

public class Alumno
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Los nombres son obligatorios.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Los nombres deben tener entre 3 y 100 caracteres.")]
    [Display(Name = "Nombres completos")]
    public string Nombres { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
    [StringLength(150, ErrorMessage = "El correo no puede superar los 150 caracteres.")]
    [Display(Name = "Correo electrónico")]
    public string Correo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La clave es obligatoria.")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "La clave debe tener entre 6 y 50 caracteres.")]
    [DataType(DataType.Password)]
    [Display(Name = "Clave de acceso")]
    public string Clave { get; set; } = string.Empty;

    [Required(ErrorMessage = "Seleccione una carrera.")]
    [Display(Name = "Carrera profesional")]
    public string Carrera { get; set; } = string.Empty;

    [Required(ErrorMessage = "Seleccione un turno.")]
    public string Turno { get; set; } = string.Empty;

    [Display(Name = "¿Cuenta con beca?")]
    public bool Becado { get; set; }

    [StringLength(500, ErrorMessage = "Las observaciones no pueden superar los 500 caracteres.")]
    public string? Observaciones { get; set; }
}
