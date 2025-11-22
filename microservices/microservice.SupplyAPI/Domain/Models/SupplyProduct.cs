namespace microservice.SupplyAPI.Domain.Models
{
    public class SupplyProduct
    {
        private Guid SupplyId;
        private Guid ProductId;
        private int Quantity;

        public SupplyProduct(Guid supplyId, Guid productId, int quantity)
        { 
            if (quantity <= 0)
            {
                throw new ArgumentException("Product quantity can't be zero or less", nameof(quantity));
            }

            SupplyId = supplyId;
            ProductId = productId;
        }

        public Guid GetSupplyId() { return SupplyId; }
        public Guid GetProductId() { return ProductId; }
        public int GetQuantity() { return Quantity; }
    }
}
