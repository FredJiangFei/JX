using System;
using System.Runtime.Serialization;

namespace JX.Product
{
    [DataContract]
    public class ProductDomain
    {
        public ProductDomain(Guid id, string name, int price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        [DataMember]
        public readonly Guid Id;

        [DataMember]
        public readonly string Name;

        [DataMember]
        public readonly int Price;
    }
}
