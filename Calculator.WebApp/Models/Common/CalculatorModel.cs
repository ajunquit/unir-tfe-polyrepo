namespace Calculator.WebApp.Models.Common
{
    public class CalculatorModel: CommonModel
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Result { get; set; }
        public string Operation { get; set; } = "Addition"; // Default operation
        public GitModel GitModel { get; set; } = new GitModel();
    }
}
