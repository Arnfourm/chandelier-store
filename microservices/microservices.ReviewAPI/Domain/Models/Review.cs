namespace microservices.ReviewAPI.Domain.Models
{
    public class Review
    {
        private Guid Id;
        private Guid UserId;
        private Guid ProductId;
        private Guid OrderId;
        private int Rate;
        private string? Content;
        private DateTime CreationDate;

        public Review(Guid userId, Guid productId, Guid orderId,
                     int rate, string? content, DateTime creationDate)
        {
            if (rate > 5 || rate < 1) throw new ArgumentException("Review rate can't be more than 5 or less than zero", nameof(rate));

            if (creationDate > DateTime.Now) throw new ArgumentException("Review creation date can't be in the future", nameof(creationDate));
            if (creationDate == default) throw new ArgumentException("Review date can't be default", nameof(creationDate));

            UserId = userId;
            ProductId = productId;
            OrderId = orderId;
            Rate = rate;
            Content = content;
            CreationDate = creationDate;
        }
        public Review(Guid id, Guid userId, Guid productId, Guid orderId,
                     int rate, string? content, DateTime creationDate)
                     : this(userId, productId, orderId, rate, content, creationDate)
        {
            Id = id;
        }

        public Guid GetId() { return Id; }
        public Guid GetUserId() { return UserId; }
        public Guid GetProductId() { return ProductId; }
        public Guid GetOrderId() { return OrderId; }
        public int GetRate() { return Rate; }
        public string? GetContent() { return Content; }
        public DateTime GetCreationDate() { return CreationDate; }
    }
}
