using Caso_práctico__1_Andrés.Domain.Entidades;
using Caso_práctico__1_Andrés.Domain.Repositorios;
using Caso_práctico__1_Andrés.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Caso_práctico__1_Andrés.Infrastructure.Repository;

public sealed class ReservaRepository(ApplicationDbContext context) : IReservaRepository
{
    public async Task<IReadOnlyList<Reserva>> ObtenerTodasAsync(CancellationToken cancellationToken = default)
    {
        return await context.Reservas
            .AsNoTracking()
            .Include(reserva => reserva.Sala)
            .OrderBy(reserva => reserva.FechaInicio)
            .ToListAsync(cancellationToken);
    }

    public Task<Reserva?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return context.Reservas
            .Include(reserva => reserva.Sala)
            .FirstOrDefaultAsync(reserva => reserva.Id == id, cancellationToken);
    }

    public Task<bool> ExisteSolapamientoAsync(int salaId, DateTime fechaInicio, DateTime fechaFin, int? reservaId = null, CancellationToken cancellationToken = default)
    {
        return context.Reservas.AnyAsync(reserva =>
            reserva.SalaId == salaId &&
            (!reservaId.HasValue || reserva.Id != reservaId.Value) &&
            fechaInicio < reserva.FechaFin &&
            fechaFin > reserva.FechaInicio,
            cancellationToken);
    }

    public async Task AgregarAsync(Reserva reserva, CancellationToken cancellationToken = default)
    {
        await context.Reservas.AddAsync(reserva, cancellationToken);
    }

    public async Task<bool> GuardarCambiosAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}
