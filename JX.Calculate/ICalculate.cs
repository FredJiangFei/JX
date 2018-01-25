using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace JX.Calculate
{
    public interface ICalculate : IService
    {
        Task<int> CalculatePriceAsync(int unitPrice, int count);

        Task<int> CalculatePlus(int a, int b);
    }
}
