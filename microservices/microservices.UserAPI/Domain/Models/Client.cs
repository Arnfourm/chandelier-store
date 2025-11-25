namespace microservices.UserAPI.Domain.Models
{
    public class Client
    {
        private Guid UserId;
        private string DeliveryAddressCountry;  
        private string DeliveryAddressDistrict;
        private string DeliveryAddressCity;
        private string DeliveryAddressStreet;
        private string DeliveryAddressHouse;
        private string DeliveryAddressPostalIndex;

        public Client(Guid userId, string country, string district,string addressCity,
                      string addressStreet, string addressHouse, string addressPostalIndex)
        {
            ValidateClientAddress(country, district, addressCity, addressStreet, addressHouse, addressPostalIndex);

            UserId = userId;
            DeliveryAddressCountry = country;
            DeliveryAddressDistrict = district;
            DeliveryAddressCity = addressCity;
            DeliveryAddressStreet = addressStreet;
            DeliveryAddressHouse = addressHouse;
            DeliveryAddressPostalIndex = addressPostalIndex;
        }

        public Guid GetUserId() { return UserId; }
        public string GetDeliveryAddressCountry() { return DeliveryAddressCountry; }
        public string GetDeliveryAddressDistrict() { return DeliveryAddressDistrict; }
        public string GetDeliveryAddressCity() { return DeliveryAddressCity; }
        public string GetDeliveryAddressStreet() { return DeliveryAddressStreet; }
        public string GetDeliveryAddressHouse() { return DeliveryAddressHouse; }
        public string GetDeliveryAddressPostalIndex() { return DeliveryAddressPostalIndex; }

        public void SetDeliveryAddress(string country, string district, string addressCity,
                                        string addressStreet, string addressHouse, string addressPostalIndex)
        {
            ValidateClientAddress(country, district, addressCity, addressStreet, addressHouse, addressPostalIndex);

            DeliveryAddressCountry = country;
            DeliveryAddressDistrict = district;
            DeliveryAddressCity = addressCity;
            DeliveryAddressStreet = addressStreet;
            DeliveryAddressHouse = addressHouse;
            DeliveryAddressPostalIndex = addressPostalIndex;
        }

        private void ValidateClientAddress(string country, string district, string addressCity,
                                           string addressStreet, string addressHouse, string addressPostalIndex)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentNullException("Country can't be null or with only zeros", nameof(country));
            if (string.IsNullOrWhiteSpace(district)) throw new ArgumentNullException("Destrict can't be null or with only zeros", nameof(district));
            if (string.IsNullOrWhiteSpace(addressCity)) throw new ArgumentNullException("City can't be null or with only zeros", nameof(addressCity));
            if (string.IsNullOrWhiteSpace(addressStreet)) throw new ArgumentNullException("Street can't be null or with only zeros", nameof(addressStreet));
            if (string.IsNullOrWhiteSpace(addressHouse)) throw new ArgumentNullException("House can't be null or with only zeros", nameof(addressHouse));
            if (string.IsNullOrWhiteSpace(addressPostalIndex)) throw new ArgumentNullException("Postgal code can't be null or with only zeros", nameof(addressPostalIndex));
        }
    }
}