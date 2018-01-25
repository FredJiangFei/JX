using System;
using System.Fabric;
using System.Threading.Tasks;
using JX.Calculate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace JX.Web.Controllers
{
    public class CalculateController : Controller
    {
        private readonly StatelessServiceContext _serviceContext;
        private readonly FabricClient _fabricClient;

        public CalculateController(StatelessServiceContext serviceContext,
            FabricClient fabricClient)
        {
            _serviceContext = serviceContext;
            _fabricClient = fabricClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Plus(int a, int b)
        {
            var serviceUri = this._serviceContext.CodePackageActivationContext.ApplicationName
                             + "/Calculate";
            var proxy = ServiceProxy.Create<ICalculate>(new Uri(serviceUri));
            var total = await proxy.CalculatePlus(a, b);
            return this.Json(total);
        }
    }
}
