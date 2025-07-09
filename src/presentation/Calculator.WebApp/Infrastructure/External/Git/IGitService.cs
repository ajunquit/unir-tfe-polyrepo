using Calculator.WebApp.Infrastructure.External.Git.Models;

namespace Calculator.WebApp.Infrastructure.External.Git
{
    public interface IGitService
    {
        public GitInfo GetSuperProjectInfo(string repoPath, string branch);
        public List<GitInfo> GetSubmodulesProjectsInfo(string repoPath);
    }
}
