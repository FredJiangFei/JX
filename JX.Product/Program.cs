using System;
using System.Threading;
using JX.Product.Events;
using Microsoft.ServiceFabric.Actors.Runtime;

namespace JX.Product
{
    internal static class Program
    {
        private static void Main()
        {
            try
            {
                ActorRuntime.RegisterActorAsync<Product>(
                   (context, actorType) => new ActorService(context, actorType)).GetAwaiter().GetResult();

                Thread.Sleep(Timeout.Infinite);

                //var bus = new FakeBus();
                //var detail = new CreateProductEvent();
                //bus.RegisterHandler<CreateProductEvent>(detail.Handle);
            }
            catch (Exception e)
            {
                ActorEventSource.Current.ActorHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
