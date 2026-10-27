using Caso_práctico__1_Andrés.Domain.Entidades;
using Caso_práctico__1_Andrés.Domain.Repositorios;
using Caso_práctico__1_Andrés.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Caso_práctico__1_Andrés.Infrastructure.Repository;

public sealed class SalaRepository(ApplicationDbContext context) : ISalaRepository
{
    public async Task<IReadOnlyList<Sala>> ObtenerTodasAsync(CancellationToken cancellationToken = default)
    {
        return await context.Salas.AsNoTracking().OrderBy(sala => sala.Nombre).ToListAsync(cancellationToken);
    }

    public Task<Sala?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return context.Salas.FirstOrDefaultAsync(sala => sala.Id == id, cancellationToken);
    }

    public async Task AgregarAsync(Sala sala, CancellationToken cancellationToken = default)
    {
        await context.Salas.AddAsync(sala, cancellationToken);
    }

    public void Actualizar(Sala sala)
    {
        context.Salas.Update(sala);
    }

    public void Eliminar(Sala sala)
    {
        context.Salas.Remove(sala);
    }

    public Task<bool> TieneReservasAsync(int salaId, CancellationToken cancellationToken = default)
    {
        return context.Reservas.AnyAsync(reserva => reserva.SalaId == salaId, cancellationToken);
    }

    public async Task<bool> GuardarCambiosAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}
