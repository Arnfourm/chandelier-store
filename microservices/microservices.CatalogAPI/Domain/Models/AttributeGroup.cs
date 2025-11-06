namespace microservices.CatalogAPI.Domain.Models
{
    public class AttributeGroup
    {
        private int Id;
        private string Title;

        public AttributeGroup(string title)
        {
            ValidateAttributeGroupTitle(title);

            Title = title;
        }
        public AttributeGroup(int id, string title)
                             : this(title)
        {
            Id = id;
        }

        public int GetId() { return Id; }
        public string GetTitle() { return Title; }

        public void SetTitle(string title) 
        {
            ValidateAttributeGroupTitle(title);

            Title = title; 
        }

        private void ValidateAttributeGroupTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("AttributeGroup title can't be null or empty: ", nameof(title));
            }
        }
    }
}
