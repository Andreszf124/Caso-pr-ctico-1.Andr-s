using Caso_práctico__1_Andrés.Domain.Entidades;

namespace Caso_práctico__1_Andrés.Domain.Repositorios;

public interface ISalaRepository
{
    Task<IReadOnlyList<Sala>> ObtenerTodasAsync(CancellationToken cancellationToken = default);

    Task<Sala?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default);

    Task AgregarAsync(Sala sala, CancellationToken cancellationToken = default);

    void Actualizar(Sala sala);

    void Eliminar(Sala sala);

    Task<bool> TieneReservasAsync(int salaId, CancellationToken cancellationToken = default);

    Task<bool> GuardarCambiosAsync(CancellationToken cancellationToken = default);
}
