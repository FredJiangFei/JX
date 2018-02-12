using JX.Product.Events;

namespace JX.Product.ReadModel
{
    public class InventoryListView : IHandles<CreateProductEvent>
    {
        public void Handle(CreateProductEvent message)
        {
            ProductDatabase.List.Add(new ProductDto(
                message.Id, 
                message.Name, 
                message.Price, 
                message.Count, 
                message.TotlaPrice));
        }
    }
}
