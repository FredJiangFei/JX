using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace JX.Customer
{
    internal sealed class Customer : StatefulService, ICustomer
    {
        public Customer(StatefulServiceContext context)
            : base(context)
        { }

      
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new ServiceReplicaListener[0];
        }

        public readonly string StateName = "Customer";

        public async Task<List<CustomerDomain>> Get()
        {
            var result = new List<CustomerDomain>();
            var tryGetResult = await StateManager.TryGetAsync<IReliableDictionary<string, CustomerDomain>>(StateName);
            if (tryGetResult.HasValue)
            {
                using (var tx = StateManager.CreateTransaction())
                {
                    var enumerable = await tryGetResult.Value.CreateEnumerableAsync(tx);
                    var enumerator = enumerable.GetAsyncEnumerator();

                    while (await enumerator.MoveNextAsync(CancellationToken.None))
                    {
                        result.Add(enumerator.Current.Value);
                    }
                }
            }

            return result;
        }

        public async Task Save(CustomerDomain customer, CancellationToken token)
        {
            var dictionary = await StateManager.GetOrAddAsync<IReliableDictionary<string, CustomerDomain>>(StateName);

            using (var tx = StateManager.CreateTransaction())
            {
                await dictionary.SetAsync(tx, customer.Name, customer);
                await tx.CommitAsync();
            }
        }
    }
}
