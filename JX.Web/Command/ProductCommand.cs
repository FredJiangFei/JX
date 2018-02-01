using System;
using System.Runtime.Serialization;

namespace JX.Web.Command
{
    public class ProductCommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public int Count { get; set; }

        public int TotalPrice { get; set; }
    }
}
