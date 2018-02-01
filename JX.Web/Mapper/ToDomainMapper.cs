using JX.Product;
using JX.Product.Interfaces;
using JX.Web.Command;

namespace JX.Web.Mapper
{
    public static class ToDomainMapper
    {
        public static ProductDomain ToDomain(this ProductCommand command)
        {
            return new ProductDomain(command.Id, command.Name, command.Price, command.Count, command.TotalPrice);
        }
    }
}
