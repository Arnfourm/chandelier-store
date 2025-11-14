namespace microservice.SupplyAPI.Domain.Models
{
    public class SupplyProduct
    {
        private Guid SupplyId;
        private Guid ProductId;

        public SupplyProduct(Guid supplyId, Guid productId)
        { 
            SupplyId = supplyId;
            ProductId = productId;
        }

        public Guid GetSupplyId() { return SupplyId; }
        public Guid GetProductId() { return ProductId; }
    }
}
