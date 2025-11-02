namespace microservices.CatalogAPI.Domain.Models
{
    public class Attribute
    {
        private Guid Id;
        private string Title;
        private int AttributeGroupId;
        private int MeasurementUnitId;

        public Attribute(string title, int attributeGroupId, int measurementUnitId) 
        {
            ValidateAttributeTitle(title);

            Title = title;
            AttributeGroupId = attributeGroupId;
            MeasurementUnitId = measurementUnitId;
        }
        public Attribute(Guid id, string title, int attributeGroupId, int measurementUnitId)
                        : this(title, attributeGroupId, measurementUnitId)
        {
            Id = id;
        }

        public Guid GetId() { return Id; }
        public string GetTitle() { return Title; }
        public int GetAttributeGroupId() { return AttributeGroupId; }
        public int GetMeasurementUnitId() { return MeasurementUnitId; }

        public void SetTitle(string title)
        {
            ValidateAttributeTitle(title);

            Title = title;
        }
        public void SetAttributeGroupId(int attributeGroupId)
        {
            AttributeGroupId = attributeGroupId;
        }

        private void ValidateAttributeTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Attribute title can't be null or empty: ", nameof(title));
            }
        }
    }
}
