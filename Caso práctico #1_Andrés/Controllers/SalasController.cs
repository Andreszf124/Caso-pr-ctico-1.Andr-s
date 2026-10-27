using Caso_práctico__1_Andrés.Application.Dtos;
using Caso_práctico__1_Andrés.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Caso_práctico__1_Andrés.Controllers;

public sealed class SalasController(ISalaService service) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        return View(await service.ObtenerTodasAsync(cancellationToken));
    }

    public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            return NotFound();
        }

        var sala = await service.ObtenerPorIdAsync(id.Value, cancellationToken);
        return sala is null ? NotFound() : View(sala);
    }

    public IActionResult Create()
    {
        return View(new SalaDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SalaDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        if (!await service.CrearAsync(dto, cancellationToken))
        {
            ModelState.AddModelError(string.Empty, "No se pudo crear la sala.");
            return View(dto);
        }

        TempData["Mensaje"] = "Sala creada correctamente.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            return NotFound();
        }

        var sala = await service.ObtenerPorIdAsync(id.Value, cancellationToken);
        return sala is null ? NotFound() : View(sala);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, SalaDto dto, CancellationToken cancellationToken)
    {
        if (id != dto.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        if (!await service.ActualizarAsync(dto, cancellationToken))
        {
            return NotFound();
        }

        TempData["Mensaje"] = "Sala actualizada correctamente.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            return NotFound();
        }

        var sala = await service.ObtenerPorIdAsync(id.Value, cancellationToken);
        return sala is null ? NotFound() : View(sala);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var resultado = await service.EliminarAsync(id, cancellationToken);
        if (!resultado.Exitoso)
        {
            TempData["Error"] = resultado.Error;
        }
        else
        {
            TempData["Mensaje"] = "Sala eliminada correctamente.";
        }

        return RedirectToAction(nameof(Index));
    }
}
