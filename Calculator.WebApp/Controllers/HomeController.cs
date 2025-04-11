using Addition.Service.Application.Interfaces;
using Calculator.WebApp.Infrastructure.External.GitHub;
using Calculator.WebApp.Models;
using Calculator.WebApp.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Calculator.WebApp.Controllers
{
    public class HomeController(
        IAdditionAppService additionAppService,
        ILogger<HomeController> logger,
        IGitRepositoryAnalyzerService gitRepositoryAnalyzerService,
        IConfiguration configuration
        ) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IGitRepositoryAnalyzerService _gitRepositoryAnalyzerService = gitRepositoryAnalyzerService;
        private readonly IConfiguration _configuration = configuration;
        private readonly IAdditionAppService _additionAppService = additionAppService;
        public CalculatorModel CalculatorModel { get; set; } = new CalculatorModel();

        public async Task<IActionResult> Index()
        {
            CalculatorModel.GitModel = await _gitRepositoryAnalyzerService.AnalyzeRepositoryAsync(_configuration.GetValue<string>("Git:Superproject"), _configuration.GetValue<string>("Git:Branch"));
            return await Task.FromResult(View(CalculatorModel));
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
                    "Subtraction" => throw new NotImplementedException("Operación no implementada"),
                    "Multiplication" => throw new NotImplementedException("Operación no implementada"),
                    "Division" => throw new NotImplementedException("Operación no implementada"),
                    _ => throw new InvalidOperationException("Operación no válida")
                };
                _logger.LogInformation("Operación resulta con éxito.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                _logger.LogError(ex, "Error en cálculo");
            }

            return View("Index", model);
        }
    }
}
