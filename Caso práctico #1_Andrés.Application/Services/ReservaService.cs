using Caso_práctico__1_Andrés.Application.Dtos;
using Caso_práctico__1_Andrés.Domain.Entidades;
using Caso_práctico__1_Andrés.Domain.Repositorios;

namespace Caso_práctico__1_Andrés.Application.Services;

public sealed class ReservaService(IReservaRepository reservaRepository, ISalaRepository salaRepository) : IReservaService
{
    public async Task<IReadOnlyList<ReservaDto>> ObtenerTodasAsync(CancellationToken cancellationToken = default)
    {
        var reservas = await reservaRepository.ObtenerTodasAsync(cancellationToken);
        return reservas.Select(reserva => new ReservaDto
        {
            Id = reserva.Id,
            Motivo = reserva.Motivo,
            SalaId = reserva.SalaId,
            SalaNombre = reserva.Sala?.Nombre ?? string.Empty,
            Encargado = reserva.Encargado,
            CantidadPersonas = reserva.CantidadPersonas,
            Dia = DateOnly.FromDateTime(reserva.FechaInicio),
            HoraInicio = TimeOnly.FromDateTime(reserva.FechaInicio),
            HoraFin = TimeOnly.FromDateTime(reserva.FechaFin),
            DuracionMinutos = (int)(reserva.FechaFin - reserva.FechaInicio).TotalMinutes,
            FechaInicio = reserva.FechaInicio,
            FechaFin = reserva.FechaFin
        }).ToList();
    }

    public async Task<(bool Exitoso, string? Error)> CrearAsync(ReservaDto dto, CancellationToken cancellationToken = default)
    {
        var fechaInicio = dto.Dia.ToDateTime(dto.HoraInicio);
        var fechaFin = fechaInicio.AddMinutes(dto.DuracionMinutos);

        if (dto.DuracionMinutos is not 30 and not 60)
        {
            return (false, "La duración debe ser de 30 minutos o 1 hora.");
        }

        if (dto.HoraInicio < new TimeOnly(10, 0) || fechaFin.TimeOfDay > new TimeOnly(17, 0).ToTimeSpan())
        {
            return (false, "Las reservas solo están disponibles entre las 10:00 y las 17:00.");
        }

        var sala = await salaRepository.ObtenerPorIdAsync(dto.SalaId, cancellationToken);
        if (sala is null)
        {
            return (false, "La sala seleccionada no existe.");
        }

        if (dto.CantidadPersonas > sala.Capacidad)
        {
            return (false, $"La sala tiene capacidad para {sala.Capacidad} personas.");
        }

        if (await reservaRepository.ExisteSolapamientoAsync(dto.SalaId, fechaInicio, fechaFin, cancellationToken: cancellationToken))
        {
            return (false, "La sala ya está reservada en el intervalo indicado.");
        }

        var reserva = new Reserva
        {
            Motivo = dto.Motivo.Trim(),
            SalaId = dto.SalaId,
            Encargado = dto.Encargado.Trim(),
            CantidadPersonas = dto.CantidadPersonas,
            FechaInicio = fechaInicio,
            FechaFin = fechaFin
        };

        await reservaRepository.AgregarAsync(reserva, cancellationToken);
        return (await reservaRepository.GuardarCambiosAsync(cancellationToken), null);
    }
}
