namespace microservices.AnalizeAPI.Domain.Models
{
    public class OrderStats
    {
        public DateTime Date { get; private set; }
        public decimal TotalAmount { get; private set; }
        public int OrderCount { get; private set; }
        public decimal AvgOrderAmount { get; private set; }

        public OrderStats(DateTime date, decimal totalAmount, int orderCount, decimal avgOrderAmount) 
        {
            if (date > DateTime.Now) throw new ArgumentException("Statistic date can't be in the future", nameof(date));
            if (date == default) throw new ArgumentException("Order date can't be default", nameof(date));

            if (totalAmount < 0) throw new ArgumentException("TotalAmount не может быть отрицательным", nameof(totalAmount));

            if (orderCount < 0) throw new ArgumentException("OrderCount не может быть отрицательным", nameof(orderCount));

            if (avgOrderAmount < 0) throw new ArgumentException("AvgOrderAmount не может быть отрицательным", nameof(avgOrderAmount));

            Date = date;
            TotalAmount = totalAmount;
            OrderCount = orderCount;
            AvgOrderAmount = avgOrderAmount;
        }
    }
}
