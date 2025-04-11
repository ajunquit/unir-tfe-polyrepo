namespace Calculator.WebApp.Models.Common
{
    public record SubmoduleInfo(string Path, string Sha, string? Url = null);

    public class GitModel
    {
        public SubmoduleInfo Superproject { get; set; } = null!;
        public List<SubmoduleInfo> Submodules { get; set; } = new List<SubmoduleInfo>();
    }
}
