using System;

namespace JX.Product.Events
{
    public class CreateProductEvent : Event
    {
        public CreateProductEvent(Guid id, string name, int price, int count, int totlaPrice)
        {
            Id = id;
            Name = name;
            Price = price;
            Count = count;
            TotlaPrice = totlaPrice;
        }

        public Guid Id;

        public string Name;

        public int Price;

        public int Count;

        public int TotlaPrice;
    }
}
