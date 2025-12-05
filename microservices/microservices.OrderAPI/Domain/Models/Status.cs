namespace microservices.OrderAPI.Domain.Models
{
    public class Status
    {
        private int Id;
        private string Title;

        public Status(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException("Status type of order can't be null or empty", nameof(title));
            }

            Title = title;
        }
        public Status(int id, string title)
                            : this(title)
        {
            Id = id;
        }

        public int GetId() { return Id; }
        public string GetTitle() { return Title; }
    }
}
