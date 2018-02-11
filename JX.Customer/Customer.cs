using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
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
            return new[] { new ServiceReplicaListener(this.CreateServiceRemotingListener) };
        }

        public readonly string StateName = "Customer";

        public async Task<List<CustomerDomain>> Get()
        {
            var result = new List<CustomerDomain>();
            var tryGetResult = await StateManager.TryGetAsync<IReliableDictionary<Guid, CustomerDomain>>(StateName);
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
            var dictionary = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, CustomerDomain>>(StateName);

            using (var tx = StateManager.CreateTransaction())
            {
                await dictionary.SetAsync(tx, customer.Id, customer);
                await tx.CommitAsync();
            }
        }

        public async Task<CustomerDomain> GetById(Guid id, CancellationToken token)
        {
            var dictionary = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, CustomerDomain>>(StateName);
            using (var tx = StateManager.CreateTransaction())
            {
                var customer = await dictionary.TryGetValueAsync(tx, id);
                return customer.Value;
            }
        }

        public async Task Delete(Guid id, CancellationToken token)
        {
            var dictionary = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, CustomerDomain>>(StateName);
            using (var tx = StateManager.CreateTransaction())
            {
                await dictionary.TryRemoveAsync(tx, id);
                await tx.CommitAsync();
            }
        }
    }
}
