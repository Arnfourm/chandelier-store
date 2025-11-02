namespace microservices.CatalogAPI.Domain.Models
{
    public class MeasurementUnit
    {
        private int Id;
        private string Title;

        public MeasurementUnit(string title)
        {
            ValidateMeasurementUnitTitle(title);

            Title = title;
        }
        public MeasurementUnit(int id, string title)
                              : this(title)
        {
            Id = id;
        }

        public int GetId() { return Id; }
        public string GetTitle() { return Title; }

        public void SetTitle(string title) 
        {
            ValidateMeasurementUnitTitle(title);

            Title = title; 
        }

        private void ValidateMeasurementUnitTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("MeasurementUnit title can't be null or empty: ", nameof(title));
            }
        }
    }
}
