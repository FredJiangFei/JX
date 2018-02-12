using System;
using System.Collections.Generic;

namespace JX.Product.ReadModel
{
    public interface IReadModel
    {
        IEnumerable<ProductDto> GetProducts();
        ProductDto GetProductById(Guid id);
    }

}
