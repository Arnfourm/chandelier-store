namespace microservices.CatalogAPI.Domain.Models
{
    public class Product
    {
        private Guid Id;
        private string Article;
        private string Title;
        private decimal Price;
        private int Quantity;
        private int? LampPower;
        private int? LampCount;
        private int ProductTypeId;
        private DateOnly AddedDate;

        public Product(string article, string title, decimal price, 
                        int quantity, int? lampPower, int? lampCount, 
                        int productTypeId, DateOnly addedDate)
        {
            if (string.IsNullOrWhiteSpace(article)) throw new ArgumentException("Product article can't be null or empty: ", nameof(article));
            if (string.IsNullOrEmpty(title)) throw new ArgumentException("Product title can't be null or empty: ", nameof(title));

            if (price <= 0) throw new ArgumentException("Product price can't be zero or less", nameof(price));
            if (quantity < 0) throw new ArgumentException("Product quantity can't be less than zero", nameof(quantity));

            if (lampPower != null && lampPower <= 0) throw new ArgumentException("Product lamp power can't be zero or less", nameof(lampPower));
            if (lampCount != null && lampCount <= 0) throw new ArgumentException("Product lamp count can't be zero or less", nameof(lampCount));

            if (addedDate > DateOnly.FromDateTime(DateTime.Now)) throw new ArgumentException("Product date add can't be in the future", nameof(addedDate));
            if (addedDate == default) throw new ArgumentException("Product date can't be default date", nameof(addedDate));

            Article = article;
            Title = title;
            Price = price;
            Quantity = quantity;
            LampPower = lampPower;
            LampCount = lampCount;
            ProductTypeId = productTypeId;
            AddedDate = addedDate;
        }

        public Product(Guid id, string article, string title, decimal price, 
                        int quantity, int? lampPower, int? lampCount,
                        int productTypeId, DateOnly addedDate) 
                        : this(article, title, price, quantity, lampPower, lampCount, productTypeId, addedDate)        
        {
            Id = id;
        }

        public Guid GetId() { return Id; }
        public string GetArticle() { return Article; }
        public string GetTitle() { return Title; }
        public decimal GetPrice() { return Price; }
        public int GetQuantity() { return Quantity; }
        public int? GetLampPower() { return LampPower; }
        public int? GetLampCount() { return LampCount; }
        public int GetProductTypeId() { return ProductTypeId; }
        public DateOnly GetAddedDate() { return AddedDate; }

        public void SetArticle(string article) { 
            if (string.IsNullOrWhiteSpace(article)) throw new ArgumentException("Product article can't be null or empty: ", nameof(article));
            
            Article = article;
        }
        public void SetTitle(string title)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentException("Product title can't be null or empty: ", nameof(title));

            Title = title;
        }
        public void SetPrice(decimal price)
        {
            if (price <= 0) throw new ArgumentException("Product price can't be zero or less", nameof(price));

            Price = price;
        }
        public void SetQuantity(int quantity) 
        {
            if (quantity < 0) throw new ArgumentException("Product quantity can't be less than zero", nameof(quantity));
            
            Quantity = quantity;
        }
        public void SetLampPower(int? lampPower)
        {
            if (lampPower != null && lampPower <= 0) throw new ArgumentException("Product lamp power can't be zero or less", nameof(lampPower));
            
            LampPower = lampPower;
        }
        public void SetLampCount(int? lampCount)
        {
            if (lampCount != null && lampCount <= 0) throw new ArgumentException("Product lamp count can't be zero or less", nameof(lampCount));

            LampCount = lampCount;
        }
        public void SetProductTypeId(int productTypeId)
        {
            ProductTypeId = productTypeId;
        }
    }
}
