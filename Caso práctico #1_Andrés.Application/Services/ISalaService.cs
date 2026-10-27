using Caso_práctico__1_Andrés.Application.Dtos;

namespace Caso_práctico__1_Andrés.Application.Services;

public interface ISalaService
{
    Task<IReadOnlyList<SalaDto>> ObtenerTodasAsync(CancellationToken cancellationToken = default);

    Task<SalaDto?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default);

    Task<bool> CrearAsync(SalaDto dto, CancellationToken cancellationToken = default);

    Task<bool> ActualizarAsync(SalaDto dto, CancellationToken cancellationToken = default);

    Task<(bool Exitoso, string? Error)> EliminarAsync(int id, CancellationToken cancellationToken = default);
}
