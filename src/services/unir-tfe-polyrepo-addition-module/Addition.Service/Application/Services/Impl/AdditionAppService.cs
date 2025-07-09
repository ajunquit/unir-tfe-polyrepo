using Addition.Service.Application.Interfaces;

namespace Addition.Service.Application.Services.Impl
{
    public class AdditionAppService : IAdditionAppService
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
