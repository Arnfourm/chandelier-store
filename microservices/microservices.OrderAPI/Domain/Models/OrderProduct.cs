namespace microservices.OrderAPI.Domain.Models
{
    public class OrderProduct
    {
        private Guid OrderId;
        private Guid ProductId;
        private decimal UnitPrice;
        private int Quantity;

        public OrderProduct(Guid orderId, Guid productId, decimal unitPrice, int quantity)
        {
            if (unitPrice <= 0)
            {
                throw new ArgumentException("Product price in order can't be zero or less", nameof(quantity));
            }
            if (quantity <= 0)
            {
                throw new ArgumentException("Product quantity in order can't be zero or less", nameof(quantity));
            }

            OrderId = orderId;
            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public Guid GetOrderId() { return OrderId; }
        public Guid GetProductId() { return ProductId; }
        public decimal GetUnitPrice() { return UnitPrice; }
        public int GetQuantity() { return Quantity; }
    }
}
