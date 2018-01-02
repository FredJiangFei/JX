﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;

namespace JX.Product.Interfaces
{
    public interface IProduct : IActor
    {
        Task<ProductDomain> Get();

        Task Create(ProductDomain command, CancellationToken token);

        Task Update(ProductDomain command, CancellationToken token);
    }
}
