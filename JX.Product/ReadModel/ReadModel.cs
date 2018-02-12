using System;
using System.Collections.Generic;

namespace JX.Product.ReadModel
{
    public class ReadModel : IReadModel
    {
        public IEnumerable<ProductDto> GetProducts()
        {
            return ProductDatabase.List;
        }

        public ProductDto GetProductById(Guid id)
        {
            return ProductDatabase.Details[id];
        }
    }
}
