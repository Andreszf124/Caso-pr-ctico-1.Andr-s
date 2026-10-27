using Caso_práctico__1_Andrés.Application.Dtos;
using Caso_práctico__1_Andrés.Application.Services;
using Caso_práctico__1_Andrés.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Caso_práctico__1_Andrés.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReservaService _reservaService;

        public HomeController(ILogger<HomeController> logger, IReservaService reservaService)
        {
            _logger = logger;
            _reservaService = reservaService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var reservas = await _reservaService.ObtenerTodasAsync(cancellationToken);
            return View(reservas);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
