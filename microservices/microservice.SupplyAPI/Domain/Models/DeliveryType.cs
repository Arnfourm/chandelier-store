namespace microservice.SupplyAPI.Domain.Models
{
    public class DeliveryType
    {
        private int Id;
        private string Title;
        private string Comment;

        public DeliveryType(string title, string comment)
        {
            ValidateTitle(title);
            ValidateTitle(comment);

            Title = title;
            Comment = comment;
        }
        public DeliveryType(int id, string title, string comment)
            : this(title, comment)
        {
            Id = id;
        }
        
        public int GetId() { return Id; }
        public string GetTitle() { return Title; }
        public string GetComment() { return Comment; }

        public void SetTitle(string title)
        {
            ValidateTitle(title);

            Title = title;
        }
        public void SetComment(string comment)
        {
            ValidateComment(comment);

            Comment = comment;
        }

        private void ValidateTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Delivery type title can't be null or empty: ", nameof(title));
            }
        }
        private void ValidateComment(string comment)
        {
            if (string.IsNullOrEmpty(comment))
            {
                throw new ArgumentException("Delivery type comment can't be null or empty: ", nameof(comment));
            }
        }
    }
}
