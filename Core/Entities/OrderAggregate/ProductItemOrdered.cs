namespace Core.Entities.OrderAggregate
{
    
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(int productItemId, string productName, string pictureUrl)
        {
            // i want to keep the order as ordered, even thogu the name may change
            // thats why i need the constructor
            // no id because it will be owned by the orderItem
            ProductItemId = productItemId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        // it will not have an id because it will be owned by the order itself
        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}