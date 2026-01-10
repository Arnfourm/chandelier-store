namespace microservices.AnalizeAPI.API.Contracts.Filters
{
    public class OrderFilter
    {
        public DateTime? startDate { get; init; }
        public DateTime? endDate { get; init; }
    }
}
