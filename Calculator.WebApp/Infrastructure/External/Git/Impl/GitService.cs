using Calculator.WebApp.Infrastructure.External.Git.Models;
using LibGit2Sharp;

namespace Calculator.WebApp.Infrastructure.External.Git.Impl
{
    public class GitService : IGitService
    {
        public List<GitInfo> GetSubmodulesProjectsInfo(string repoPath)
        {
            var repository = new Repository(repoPath);
            List <GitInfo> submodules = new List<GitInfo> ();
            foreach (var submodule in repository.Submodules)
            {
                var submodulePath = Path.Combine(repoPath, submodule.Path);
                if (Directory.Exists(submodulePath))
                {
                    using (var subRepo = new Repository(submodulePath))
                    {
                        var headCommit = subRepo.Head.Tip;

                        submodules.Add(new GitInfo
                        {
                            Sha = headCommit.Sha,
                            Message = headCommit.MessageShort
                        });
                    }
                }
                else
                {
                    submodules.Add(new GitInfo
                    {
                        Sha = "--",
                        Message = "--"
                    });
                }
            }
            return submodules;
        }

        public GitInfo GetSuperProjectInfo(string repoPath, string branch)
        {
            using (var repo = new Repository(repoPath))
            {
                var masterBranch = repo.Branches[branch] ?? throw new Exception($"Rama {branch}  no encontrada.");
                return new GitInfo { Sha = masterBranch.Tip.Sha, Message = masterBranch.Tip.MessageShort };
            }
        }
    }
}
