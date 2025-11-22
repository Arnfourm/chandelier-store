namespace microservice.SupplyAPI.Domain.Models
{
    public class Product
    {
        private Guid Id;
        private string Article;
        private string Title;
        private decimal Price;
        
        public Product(Guid id, string article, string title, decimal price)
        {
            if (string.IsNullOrWhiteSpace(article)) throw new ArgumentException("Product article can't be null or empty: ", nameof(article));
            if (string.IsNullOrEmpty(title)) throw new ArgumentException("Product title can't be null or empty: ", nameof(title));

            if (price <= 0) throw new ArgumentException("Product price can't be zero or less", nameof(price));

            Id = id;
            Article = article;
            Title = title;
            Price = price;
        }

        public Guid GetId() { return Id; }
        public string GetArticle() { return Article; }
        public string GetTitle() { return Title; }
        public decimal GetPrice() { return Price; }

        public void SetArticle(string article)
        {
            if (string.IsNullOrWhiteSpace(article)) throw new ArgumentException("Product article can't be null or empty: ", nameof(article));

            Article = article;
        }
        public void SetTitle(string title)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentException("Product title can't be null or empty: ", nameof(title));

            Title = title;
        }
    }
}
