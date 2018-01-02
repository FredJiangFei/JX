using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using JX.Product.Interfaces;

namespace JX.Product
{
    [StatePersistence(StatePersistence.Persisted)]
    internal class Product : Actor, IProduct
    {
       
        public Product(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        public readonly string StateName = "Product";

        public Task<ProductDomain> Get()
        {
            var result = StateManager.GetStateAsync<ProductDomain>(StateName);
            return result;
        }

        public async Task Create(ProductDomain product, CancellationToken token)
        {
            await StateManager.TryAddStateAsync<ProductDomain>(StateName, product, token);
        }

        public async Task Update(ProductDomain product, CancellationToken token)
        {
            await StateManager.SetStateAsync(StateName, product, token);
        }
    }
}
