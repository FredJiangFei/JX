using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JX.Customer
{
    public interface ICustomer
    {
        Task<List<CustomerDomain>> Get();

        Task Save(CustomerDomain product, CancellationToken token);

    }
}
