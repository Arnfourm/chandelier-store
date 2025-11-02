namespace microservices.UserAPI.Domain.Models
{
    public class Employee
    {
        private Guid UserId;
        private int Code;
        private string ResidenceAddressCountry;  
        private string ResidenceddressDistrict;
        private string ResidenceAddressCity;
        private string ResidenceAddressStreet;
        private string ResidenceAddressHouse;
        private string ResidenceAddressPostalCode;

        public Employee(int code, string country, string district,string addressCity,
                      string addressStreet, string addressHouse, string addressPostalCode)
        {
            if (code < 0) throw new ArgumentOutOfRangeException("Employee code can't be less than zero", nameof(code));
            ValidateResidenceAddress(country, district, addressCity, addressStreet, addressHouse, addressPostalCode);

            Code = code;
            ResidenceAddressCountry = country;
            ResidenceddressDistrict = district;
            ResidenceAddressCity = addressCity;
            ResidenceAddressStreet = addressStreet;
            ResidenceAddressHouse = addressHouse;
            ResidenceAddressPostalCode = addressPostalCode;
        }

        public Employee(Guid userId, string country, string district, string addressCity,
                      string addressStreet, string addressHouse, string addressPostalCode)
        {
            UserId = userId;
            ResidenceAddressCountry = country;
            ResidenceddressDistrict = district;
            ResidenceAddressCity = addressCity;
            ResidenceAddressStreet = addressStreet;
            ResidenceAddressHouse = addressHouse;
            ResidenceAddressPostalCode = addressPostalCode;
        }

        public Guid GetUserId() { return UserId; }
        public string GetResidenceAddressCountry() { return ResidenceAddressCountry; }
        public string GetResidenceAddressDistrict() { return ResidenceddressDistrict; }
        public string GetResidenceAddressCity() { return ResidenceAddressCity; }
        public string GetResidenceAddressStreet() { return ResidenceAddressStreet; }
        public string GetResidenceAddressHouse() { return ResidenceAddressHouse; }
        public string GetResidenceAddressPostalCode() { return ResidenceAddressPostalCode; }

        public void SetCode(int code)
        {
            if (code < 0) throw new ArgumentOutOfRangeException("Employee code can't be less than zero", nameof(code));

            Code = code;
        }
        public void SetResidenceAddress(string country, string district, string addressCity,
                                        string addressStreet, string addressHouse, string addressPostalCode)
        {
            ValidateResidenceAddress(country, district, addressCity, addressStreet, addressHouse, addressPostalCode);
            ResidenceAddressCountry = country;
            ResidenceddressDistrict = district;
            ResidenceAddressCity = addressCity;
            ResidenceAddressStreet = addressStreet;
            ResidenceAddressHouse = addressHouse;
            ResidenceAddressPostalCode = addressPostalCode;
        }

        private void ValidateResidenceAddress(string country, string district, string addressCity,
                                           string addressStreet, string addressHouse, string addressPostalCode)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentNullException("Country can't be null or with only zeros", nameof(country));
            if (string.IsNullOrWhiteSpace(district)) throw new ArgumentNullException("Destrict can't be null or with only zeros", nameof(district));
            if (string.IsNullOrWhiteSpace(addressCity)) throw new ArgumentNullException("City can't be null or with only zeros", nameof(addressCity));
            if (string.IsNullOrWhiteSpace(addressStreet)) throw new ArgumentNullException("Street can't be null or with only zeros", nameof(addressStreet));
            if (string.IsNullOrWhiteSpace(addressHouse)) throw new ArgumentNullException("House can't be null or with only zeros", nameof(addressHouse));
            if (string.IsNullOrWhiteSpace(addressPostalCode)) throw new ArgumentNullException("Postgal code can't be null or with only zeros", nameof(addressPostalCode));
        }
    }
}