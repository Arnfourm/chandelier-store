namespace microservices.OrderAPI.Domain.Models
{
    public class Order
    {
        private Guid Id;
        private Guid UserId;
        private decimal TotalAmount;
        private int StatusId;
        private int DeliveryTypeId;
        private DateTime CreationDate;

        public Order(Guid userId, decimal totalAmount, int statusId, 
                    int deliveryTypeId, DateTime creationDate)
        {
            if (totalAmount < 0) throw new ArgumentException("Order total amount can't be less than zero", nameof(totalAmount));

            if (creationDate > DateTime.Now) throw new ArgumentException("Order creation date can't be in the future", nameof(creationDate));
            if (creationDate == default) throw new ArgumentException("Order date can't be default", nameof(creationDate));

            UserId = userId;
            TotalAmount = totalAmount;
            StatusId = statusId;
            DeliveryTypeId = deliveryTypeId;
            CreationDate = creationDate;
        }
        public Order(Guid id, Guid userId, decimal totalAmount, 
                    int statusId, int deliveryTypeId, DateTime creationDate)
                    : this(userId, totalAmount, statusId, deliveryTypeId, creationDate)
        {
            Id = id;
        }

        public Guid GetId() { return Id; }
        public Guid GetUserId() { return UserId; }
        public decimal GetTotalAmount() { return TotalAmount; }
        public int GetStatusId() { return  StatusId; }
        public int GetDeliveryTypeId() { return DeliveryTypeId; }
        public DateTime GetCreationDate() { return CreationDate; }
    }
}
