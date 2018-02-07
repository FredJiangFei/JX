using JX.Customer;
using JX.Product;
using JX.Web.Command;
using JX.Web.ViewModels;

namespace JX.Web.Mapper
{
    public static class ToViewModelMapper
    {
        public static ProductViewModel ToViewModel(this ProductDomain domain)
        {
            return new ProductViewModel
            {
                Id = domain.Id,
                Name = domain.Name,
                Count = domain.Count,
                TotalPrice = domain.TotlaPrice,
                Price = domain.Price
            };
        }

        public static CustomerViewModel ToViewModel(this CustomerDomain domain)
        {
            return new CustomerViewModel
            {
                Id = domain.Id,
                Name = domain.Name,
                Age = domain.Age
            };
        }

        public static ProductCommand ToCommand(this ProductDomain domain)
        {
            return new ProductCommand
            {
                Id = domain.Id,
                Name = domain.Name,
                Count = domain.Count,
                Price = domain.Price,
                TotalPrice = domain.TotlaPrice
            };
        }
    }
}
