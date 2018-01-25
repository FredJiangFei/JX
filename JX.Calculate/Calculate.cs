using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace JX.Calculate
{
    internal sealed class Calculate : StatelessService, ICalculate
    {
        public Calculate(StatelessServiceContext context)
            : base(context)
        { }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new[]
            {
                new ServiceInstanceListener(this.CreateServiceRemotingListener)
            };
        }

        public Task<int> CalculatePriceAsync(int unitPrice, int count)
        {
            return Task.FromResult(unitPrice * count);
        }

        public Task<int> CalculatePlus(int a, int b)
        {
            return Task.FromResult(a + b);
        }
    }
}
