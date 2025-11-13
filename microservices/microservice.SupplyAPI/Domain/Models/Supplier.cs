namespace microservice.SupplyAPI.Domain.Models
{
    public class Supplier
    {
        private Guid Id;
        private string Name;
        private int DeliveryTypeId;

        public Supplier(string name, int deliveryTypeId)
        {
            ValidateName(name);

            Name = name;
            DeliveryTypeId = deliveryTypeId;
        }
        public Supplier(Guid id, string name, int deliveryTypeId)
                        : this(name, deliveryTypeId)
        {
            Id = id;
        }

        public Guid GetId() { return Id; }
        public string GetName() { return Name; }
        public int GetDeliveryTypeId() { return DeliveryTypeId; }

        public void SetName(string name)
        {
            ValidateName(name);

            Name = name; 
        }
        public void SetDeliveryTypeId(int deliveryTypeId)
        {
            DeliveryTypeId = deliveryTypeId;
        }


        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Supplier name can't be null or empty: ", nameof(name));
            }
        }
    }
}
