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
                Price = domain.Price
            };
        }

        public static ProductCommand ToCommand(this ProductDomain domain)
        {
            return new ProductCommand
            {
                Id = domain.Id,
                Name = domain.Name,
                Price = domain.Price
            };
        }
    }
}
