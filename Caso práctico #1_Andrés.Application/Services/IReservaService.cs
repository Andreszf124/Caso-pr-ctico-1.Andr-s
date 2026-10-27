using Caso_práctico__1_Andrés.Application.Dtos;

namespace Caso_práctico__1_Andrés.Application.Services;

public interface IReservaService
{
    Task<IReadOnlyList<ReservaDto>> ObtenerTodasAsync(CancellationToken cancellationToken = default);

    Task<(bool Exitoso, string? Error)> CrearAsync(ReservaDto dto, CancellationToken cancellationToken = default);
}
