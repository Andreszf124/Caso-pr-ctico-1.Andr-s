using System.ComponentModel.DataAnnotations;

namespace Caso_práctico__1_Andrés.Application.Dtos;

public sealed class SalaDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Range(0, 30, ErrorMessage = "La capacidad debe estar entre 0 y 30 personas.")]
    public int Capacidad { get; set; }

    [Required(ErrorMessage = "La ubicación es obligatoria.")]
    [StringLength(150)]
    public string Ubicacion { get; set; } = string.Empty;
}
