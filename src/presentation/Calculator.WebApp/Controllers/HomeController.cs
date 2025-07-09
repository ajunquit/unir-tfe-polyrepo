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

        public CalculatorModel CalculatorModel { get; set; } = new();

        public async Task<IActionResult> Index()
        {
            await LoadGitRepositoryInfo();
            return View(CalculatorModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        [HttpPost]
        public async Task<IActionResult> Calculate(CalculatorModel model)
        {
            try
            {
                model.Result = CalculateOperation(model);
                await LoadGitRepositoryInfo(model);
                _logger.LogInformation("Operation completed successfully.");
            }
            catch (Exception ex)
            {
                HandleCalculationError(ex);
            }

            return View("Index", model);
        }

        private int CalculateOperation(CalculatorModel model)
        {
            return model.Operation switch
            {
                "Addition" => _additionAppService.Add(model.Number1, model.Number2),
                "Subtraction" => throw new NotImplementedException("Operation not implemented"),
                "Multiplication" => throw new NotImplementedException("Operation not implemented"),
                "Division" => throw new NotImplementedException("Operation not implemented"),
                _ => throw new InvalidOperationException("Invalid operation")
            };
        }

        private async Task LoadGitRepositoryInfo(CalculatorModel model = null)
        {
            var gitModel = await _gitRepositoryAnalyzerService.AnalyzeRepositoryAsync(
                _configuration.GetValue<string>("Git:Superproject")!,
                _configuration.GetValue<string>("Git:Branch")!);

            if (model == null)
            {
                CalculatorModel.GitModel = gitModel;
            }
            else
            {
                model.GitModel = gitModel;
            }
        }

        private void HandleCalculationError(Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            _logger.LogError(ex, "Calculation error");
        }
    }
}