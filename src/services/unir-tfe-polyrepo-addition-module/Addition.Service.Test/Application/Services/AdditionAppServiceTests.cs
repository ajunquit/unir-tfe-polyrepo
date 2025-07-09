using Addition.Service.Application.Interfaces;
using Addition.Service.Application.Services.Impl;

namespace Addition.Service.Test.Application.Services
{
    public class AdditionAppServiceTests
    {
        private readonly IAdditionAppService _additionAppService;

        public AdditionAppServiceTests()
        {
            _additionAppService = new AdditionAppService();
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(0, 0, 0)]
        [InlineData(-1, -1, -2)]
        [InlineData(int.MaxValue, 1, int.MinValue)] // Overflow case
        public void Addition_ShouldReturnCorrectResult(int a, int b, int expected)
        {
            // Act
            var result = _additionAppService.Add(a, b);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
