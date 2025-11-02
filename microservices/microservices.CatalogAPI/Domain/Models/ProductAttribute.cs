using System.Reflection.Metadata;

namespace microservices.CatalogAPI.Domain.Models
{
    public class ProductAttribute
    {
        private Guid ProductId;
        private Guid AttributeId;
        private string Value;

        public ProductAttribute(Guid productId, Guid attributeId, string value)
        {
            ValidateProductAttributeValue(value);

            ProductId = productId;
            AttributeId = attributeId;
            Value = value;
        }

        public Guid GetProductId() { return ProductId; }
        public Guid GetAttributeId() { return AttributeId; }
        public string GetValue() { return Value; }

        public string SetValud(string value)
        {
            ValidateProductAttributeValue(value);

            return Value = value;
        }

        private void ValidateProductAttributeValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value)){
                throw new ArgumentException("ProductAttribute value can't be null or empty", nameof(value));
            }
        }
    }
}
