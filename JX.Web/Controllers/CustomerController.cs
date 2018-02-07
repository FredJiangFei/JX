using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JX.Customer;
using JX.Web.Command;
using JX.Web.Mapper;
using JX.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace JX.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly StatelessServiceContext _serviceContext;
        private readonly FabricClient _fabricClient;

        public CustomerController(StatelessServiceContext serviceContext,
            FabricClient fabricClient)
        {
            _serviceContext = serviceContext;
            _fabricClient = fabricClient;
        }

        public async Task<IActionResult> Index()
        {
            var serviceUri = CustomerServiceUri();
            var partitions = await _fabricClient.QueryManager.GetPartitionListAsync(new Uri(serviceUri));
            var result = new List<CustomerViewModel>();

            foreach (var p in partitions)
            {
                var proxy = GetServiceProxy(((Int64RangePartitionInformation)p.PartitionInformation).LowKey);
                var customers = await proxy.Get();
                result.AddRange(customers.Select(c => c.ToViewModel()));
            }
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerCommand command)
        {
            var serviceProxy = GetServiceProxy(command.Name);
            command.Id = Guid.NewGuid();
            await serviceProxy.Save(command.ToDomain(), CancellationToken.None);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id, string name)
        {
            var serviceProxy = GetServiceProxy(name);
            var customer =  serviceProxy.GetById(id, CancellationToken.None).Result;
            return View(customer.ToViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerCommand command)
        {
            var serviceProxy = GetServiceProxy(command.Name);
            await serviceProxy.Save(command.ToDomain(), CancellationToken.None);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id, string name)
        {
            var serviceProxy = GetServiceProxy(name);
            await serviceProxy.Delete(id, CancellationToken.None);
            return RedirectToAction("Index");
        }

        private ICustomer GetServiceProxy(long keyNumber)
        {
            var serviceUri = CustomerServiceUri();
            var serviceProxy = ServiceProxy.Create<ICustomer>(new Uri(serviceUri), new ServicePartitionKey(keyNumber));
            return serviceProxy;
        }

        private ICustomer GetServiceProxy(string key)
        {
            long keyNumber = GetPartitionKey(key);
            return GetServiceProxy(keyNumber);
        }

        private string CustomerServiceUri()
        {
            return this._serviceContext.CodePackageActivationContext.ApplicationName
                + "/Customer";
        }

        private static int GetPartitionKey(string key)
        {
            char firstLetterOfKey = key.First();
            int partitionKeyInt = Char.ToUpper(firstLetterOfKey) - 'A';
            if (partitionKeyInt < 0 || partitionKeyInt > 25)
            {
                throw new ArgumentException("The key must begin with a letter between A and Z");
            }
            return partitionKeyInt;
        }
    }
}
