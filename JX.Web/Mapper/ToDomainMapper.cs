using System;
using JX.Customer;
using JX.Product;
using JX.Web.Command;

namespace JX.Web.Mapper
{
    public static class ToDomainMapper
    {
        public static ProductDomain ToDomain(this ProductCommand command)
        {
            return new ProductDomain(command.Id, command.Name, command.Price, command.Count, command.TotalPrice);
        }

        public static CustomerDomain ToDomain(this CustomerCommand command)
        {
            return new CustomerDomain(command.Id, command.Name, command.Age);
        }
    }
}
