using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JX.Calculate;
using JX.Product.Interfaces;
using JX.Web.Command;
using JX.Web.Mapper;
using JX.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Actors.Query;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace JX.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly StatelessServiceContext _serviceContext;
        private readonly FabricClient _fabricClient;

        public ProductController(StatelessServiceContext serviceContext,
            FabricClient fabricClient)
        {
            _serviceContext = serviceContext;
            _fabricClient = fabricClient;
        }

        public async Task<IActionResult> Index()
        {
            var serviceUri = GetServiceUri();
            var partitions = await this._fabricClient.QueryManager.GetPartitionListAsync(new Uri(serviceUri));
            var products = new List<ProductViewModel>();

            foreach (var p in partitions)
            {
                var partitionKey = ((Int64RangePartitionInformation)p.PartitionInformation).LowKey;
                var serviceProxy = ActorServiceProxy.Create(new Uri(serviceUri), partitionKey);
                ContinuationToken ct = null;

                do
                {
                    var page = await serviceProxy.GetActorsAsync(ct, CancellationToken.None);
                    var items = page.Items.Where(x => x.IsActive);

                    foreach (var i in items)
                    {
                        var proxy = GetProxy(i.ActorId);
                        var product = proxy.Get().Result.ToViewModel();
                        product.Id = i.ActorId.GetGuidId();
                        products.Add(product);
                    }
                    ct = page.ContinuationToken;
                }
                while (ct != null);
            }

            return View(products.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCommand command)
        {
            var productId = Guid.NewGuid();
            var proxy = GetProxy(new ActorId(productId));
            await proxy.Create(command.ToDomain(), CancellationToken.None);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            var proxy = GetProxy(new ActorId(id));
            var product = proxy.Get().Result.ToCommand();
            product.Id = id;
            return View(product);
        }
      
        [HttpPost]
        public async Task<IActionResult> Edit(ProductCommand command)
        {
            var proxy = GetProxy(new ActorId(command.Id));
            await proxy.Update(command.ToDomain(), CancellationToken.None);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var serviceUri = GetServiceUri();
            var actorToDelete = new ActorId(id);

            var proxy = ActorServiceProxy.Create(new Uri(serviceUri), actorToDelete);
            await proxy.DeleteActorAsync(actorToDelete, CancellationToken.None);
            return RedirectToAction("Index");
        }

        public IActionResult Detail(Guid id)
        {
            var proxy = GetProxy(new ActorId(id));
            var product = proxy.Get().Result.ToCommand();
            product.Id = id;
            return View(product);
        }

        public async Task<IActionResult> CalculateTotalPrice(int unitPrice, int count)
        {
            var serviceUri = this._serviceContext.CodePackageActivationContext.ApplicationName
                             + "/Calculate";
            var proxy = ServiceProxy.Create<ICalculate>(new Uri(serviceUri));
            var totalPrice = await proxy.CalculatePriceAsync(unitPrice, count);

            return this.Json(totalPrice);
        }

        private IProduct GetProxy(ActorId actorId)
        {
            var serviceUri = GetServiceUri();
            var proxy = ActorProxy.Create<IProduct>(actorId, new Uri(serviceUri));
            return proxy;
        }

        private string GetServiceUri()
        {
            return this._serviceContext.CodePackageActivationContext.ApplicationName 
                + "/ProductActorService";
        }
    }
}
