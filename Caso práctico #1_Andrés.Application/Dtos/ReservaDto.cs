using System.ComponentModel.DataAnnotations;

namespace Caso_práctico__1_Andrés.Application.Dtos;

public sealed class ReservaDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El motivo de la reserva es obligatorio.")]
    [StringLength(100)]
    public string Motivo { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una sala.")]
    public int SalaId { get; set; }

    public string SalaNombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El encargado de la reserva es obligatorio.")]
    [StringLength(150)]
    public string Encargado { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad de personas debe ser mayor que cero.")]
    public int CantidadPersonas { get; set; } = 1;

    [DataType(DataType.Date)]
    public DateOnly Dia { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    public TimeOnly HoraInicio { get; set; } = new(8, 0);

    public TimeOnly HoraFin { get; set; } = new(8, 30);

    [Range(30, 60, ErrorMessage = "La duración debe ser de 30 minutos o 1 hora.")]
    public int DuracionMinutos { get; set; } = 30;

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }
}
