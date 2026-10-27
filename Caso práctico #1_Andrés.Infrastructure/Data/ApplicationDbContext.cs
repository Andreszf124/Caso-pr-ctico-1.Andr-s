using Caso_práctico__1_Andrés.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Caso_práctico__1_Andrés.Infrastructure.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Sala> Salas => Set<Sala>();

    public DbSet<Reserva> Reservas => Set<Reserva>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sala>(entity =>
        {
            entity.Property(sala => sala.Nombre).HasMaxLength(100).IsRequired();
            entity.Property(sala => sala.Ubicacion).HasMaxLength(150).IsRequired();
            entity.HasIndex(sala => sala.Nombre).IsUnique();
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.Property(reserva => reserva.Motivo).HasMaxLength(100).IsRequired();
            entity.Property(reserva => reserva.Encargado).HasMaxLength(150).IsRequired();
            entity.Property(reserva => reserva.CantidadPersonas).IsRequired();
            entity.HasIndex(reserva => new { reserva.SalaId, reserva.FechaInicio, reserva.FechaFin });
            entity.HasOne(reserva => reserva.Sala)
                .WithMany(sala => sala.Reservas)
                .HasForeignKey(reserva => reserva.SalaId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
