namespace Caso_práctico__1_Andrés.Domain.Entidades;

public sealed class Sala
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public int Capacidad { get; set; }

    public string Ubicacion { get; set; } = string.Empty;

    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
