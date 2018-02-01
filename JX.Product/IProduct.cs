using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;

namespace JX.Product
{
    public interface IProduct : IActor
    {
        Task<ProductDomain> Get();

        Task Create(ProductDomain product, CancellationToken token);

        Task Update(ProductDomain product, CancellationToken token);
    }
}
