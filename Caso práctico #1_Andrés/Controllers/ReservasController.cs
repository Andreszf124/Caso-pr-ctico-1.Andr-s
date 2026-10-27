using Caso_práctico__1_Andrés.Application.Dtos;
using Caso_práctico__1_Andrés.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Caso_práctico__1_Andrés.Controllers;

public sealed class ReservasController(IReservaService reservaService, ISalaService salaService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        return View(await reservaService.ObtenerTodasAsync(cancellationToken));
    }

    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        await CargarSalasAsync(cancellationToken);
        return View(new ReservaDto
        {
            Dia = DateOnly.FromDateTime(DateTime.Today),
            HoraInicio = new TimeOnly(10, 0),
            HoraFin = new TimeOnly(10, 30),
            DuracionMinutos = 30,
            CantidadPersonas = 1
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ReservaDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await CargarSalasAsync(cancellationToken);
            return View(dto);
        }

        var resultado = await reservaService.CrearAsync(dto, cancellationToken);
        if (!resultado.Exitoso)
        {
            ModelState.AddModelError(string.Empty, resultado.Error ?? "No se pudo crear la reserva.");
            await CargarSalasAsync(cancellationToken);
            return View(dto);
        }

        TempData["Mensaje"] = "Reserva creada correctamente.";
        return RedirectToAction(nameof(Index));
    }

    private async Task CargarSalasAsync(CancellationToken cancellationToken)
    {
        var salas = await salaService.ObtenerTodasAsync(cancellationToken);
        ViewBag.Salas = salas.Select(sala => new SelectListItem
        {
            Value = sala.Id.ToString(),
            Text = $"{sala.Nombre} - {sala.Ubicacion} (capacidad: {sala.Capacidad})"
        }).ToList();
    }
}
