using Addition.Service.Application.Interfaces;
using Calculator.WebApp.Models;
using Calculator.WebApp.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Calculator.WebApp.Controllers
{
    public class HomeController(
        IAdditionAppService additionAppService,
        ILogger<HomeController> logger
        ) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IAdditionAppService _additionAppService = additionAppService;

        public IActionResult Index()
        {
            return View();
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

        [HttpPost]
        public IActionResult Calculate(CalculatorModel model)
        {
            try
            {
                model.Result = model.Operation switch
                {
                    "Addition" => _additionAppService.Add(model.Number1, model.Number2),
                    "Subtraction" => throw new NotImplementedException("Operaci�n no implementada"),
                    "Multiplication" => throw new NotImplementedException("Operaci�n no implementada"),
                    "Division" => throw new NotImplementedException("Operaci�n no implementada"),
                    _ => throw new InvalidOperationException("Operaci�n no v�lida")
                };
                _logger.LogInformation("Operaci�n resulta con �xito.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                _logger.LogError(ex, "Error en c�lculo");
            }

            return View("Index", model);
        }
    }
}
