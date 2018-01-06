using System;
using System.Runtime.Serialization;

namespace JX.Web.Command
{
    [DataContract]
    public class ProductCommand
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Price { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}
