namespace microservices.OrderAPI.Domain.Models
{
    public class DeliveryType
    {
        private int Id;
        private string Title;

        public DeliveryType(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException("Delivery type title of order can't be null or empty", nameof(title));
            }

            Title = title;
        }
        public DeliveryType(int id, string title)
                            : this(title)
        {
            Id = id;
        }

        public int GetId() { return Id; }
        public string GetTitle() { return Title; }
    }
}
