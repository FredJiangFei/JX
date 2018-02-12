using System;
using System.Collections.Generic;

namespace JX.Product.ReadModel
{
    public static class ProductDatabase
    {
        public static Dictionary<Guid, ProductDto> Details = new Dictionary<Guid, ProductDto>();
        public static List<ProductDto> List = new List<ProductDto>();
    }
}
