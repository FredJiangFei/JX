﻿using System;
using System.Runtime.Serialization;

namespace JX.Product.ReadModel
{
    [DataContract]
    public class ProductDto
    {
        public ProductDto(Guid id, string name, int price,int count, int totlaPrice)
        {
            Id = id;
            Name = name;
            Price = price;
            Count = count;
            TotlaPrice = totlaPrice;
        }

        [DataMember]
        public readonly Guid Id;

        [DataMember]
        public readonly string Name;

        [DataMember]
        public readonly int Price;

        [DataMember]
        public readonly int Count;

        [DataMember]
        public readonly int TotlaPrice;
    }
}
