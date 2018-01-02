using System;
using System.Runtime.Serialization;

namespace JX.Product
{
    [DataContract]
    public class ProductDomain
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Price { get; set; }
    }
}
