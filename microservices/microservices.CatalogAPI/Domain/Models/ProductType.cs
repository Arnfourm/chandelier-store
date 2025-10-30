using System.Globalization;

namespace microservices.CatalogAPI.Domain.Models
{
    public class ProductType
    {
        private int Id;
        private string Title;

        public ProductType(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("Product title can't be null or empty");
            }

            Title = title;
        }

        public ProductType(int id, string title)
        {
            Id = id;

            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("Product title can't be null or empty");
            }

            Title = title;
        }

        public int GetId() { return Id; }
        public string GetTitle() { return Title; }

        public void SetTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("Product title can't be null or empty");
            }

            Title = title;
        }
    }
}
