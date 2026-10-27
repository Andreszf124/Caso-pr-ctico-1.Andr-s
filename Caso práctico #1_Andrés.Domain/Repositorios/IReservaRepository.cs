using Caso_práctico__1_Andrés.Domain.Entidades;

namespace Caso_práctico__1_Andrés.Domain.Repositorios;

public interface IReservaRepository
{
    Task<IReadOnlyList<Reserva>> ObtenerTodasAsync(CancellationToken cancellationToken = default);

    Task<Reserva?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default);

    Task<bool> ExisteSolapamientoAsync(int salaId, DateTime fechaInicio, DateTime fechaFin, int? reservaId = null, CancellationToken cancellationToken = default);

    Task AgregarAsync(Reserva reserva, CancellationToken cancellationToken = default);

    Task<bool> GuardarCambiosAsync(CancellationToken cancellationToken = default);
}
