using Caso_práctico__1_Andrés.Application.Dtos;
using Caso_práctico__1_Andrés.Domain.Entidades;
using Caso_práctico__1_Andrés.Domain.Repositorios;

namespace Caso_práctico__1_Andrés.Application.Services;

public sealed class SalaService(ISalaRepository repository) : ISalaService
{
    public async Task<IReadOnlyList<SalaDto>> ObtenerTodasAsync(CancellationToken cancellationToken = default)
    {
        var salas = await repository.ObtenerTodasAsync(cancellationToken);
        return salas.Select(Mapear).ToList();
    }

    public async Task<SalaDto?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var sala = await repository.ObtenerPorIdAsync(id, cancellationToken);
        return sala is null ? null : Mapear(sala);
    }

    public async Task<bool> CrearAsync(SalaDto dto, CancellationToken cancellationToken = default)
    {
        var sala = new Sala
        {
            Nombre = dto.Nombre.Trim(),
            Capacidad = dto.Capacidad,
            Ubicacion = dto.Ubicacion.Trim()
        };

        await repository.AgregarAsync(sala, cancellationToken);
        return await repository.GuardarCambiosAsync(cancellationToken);
    }

    public async Task<bool> ActualizarAsync(SalaDto dto, CancellationToken cancellationToken = default)
    {
        var sala = await repository.ObtenerPorIdAsync(dto.Id, cancellationToken);
        if (sala is null)
        {
            return false;
        }

        sala.Nombre = dto.Nombre.Trim();
        sala.Capacidad = dto.Capacidad;
        sala.Ubicacion = dto.Ubicacion.Trim();
        repository.Actualizar(sala);
        return await repository.GuardarCambiosAsync(cancellationToken);
    }

    public async Task<(bool Exitoso, string? Error)> EliminarAsync(int id, CancellationToken cancellationToken = default)
    {
        var sala = await repository.ObtenerPorIdAsync(id, cancellationToken);
        if (sala is null)
        {
            return (false, "La sala no existe.");
        }

        if (await repository.TieneReservasAsync(id, cancellationToken))
        {
            return (false, "No se puede eliminar una sala que tiene reservas.");
        }

        repository.Eliminar(sala);
        return (await repository.GuardarCambiosAsync(cancellationToken), null);
    }

    private static SalaDto Mapear(Sala sala) => new()
    {
        Id = sala.Id,
        Nombre = sala.Nombre,
        Capacidad = sala.Capacidad,
        Ubicacion = sala.Ubicacion
    };
}
