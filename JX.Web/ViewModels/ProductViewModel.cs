using System;

namespace JX.Web.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }
        public int Count { get; set; }
        public int TotalPrice { get; set; }
    }
}
