using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace JX.Customer
{
    public interface ICustomer : IService
    {
        Task<List<CustomerDomain>> Get();

        Task Save(CustomerDomain customer, CancellationToken token);

        Task<CustomerDomain> GetById(Guid id, CancellationToken token);

        Task Delete(Guid id, CancellationToken token);

    }
}
