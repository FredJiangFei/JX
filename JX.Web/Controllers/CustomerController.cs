using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JX.Calculate;
using JX.Product;
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
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCommand command)
        {
            return RedirectToAction("Index");
        }

        private string CustomerServiceUri()
        {
            return this._serviceContext.CodePackageActivationContext.ApplicationName 
                + "/Customer";
        }
    }
}
