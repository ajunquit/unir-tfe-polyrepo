using Calculator.WebApp.Models.Common;

namespace Calculator.WebApp.Infrastructure.External.GitHub
{
    public interface IGitRepositoryAnalyzerService
    {
        Task<GitModel> AnalyzeRepositoryAsync(string repoUrl, string branch, string? token = null);
    }
}
