namespace Caso_práctico__1_Andrés.Domain.Entidades;

public sealed class Reserva
{
    public int Id { get; set; }

    public string Motivo { get; set; } = string.Empty;

    public int SalaId { get; set; }

    public Sala Sala { get; set; } = null!;

    public string Encargado { get; set; } = string.Empty;

    public int CantidadPersonas { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public bool SeSuperponeCon(DateTime inicio, DateTime fin)
    {
        return inicio < FechaFin && fin > FechaInicio;
    }
}
