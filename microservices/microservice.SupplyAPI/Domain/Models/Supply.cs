namespace microservice.SupplyAPI.Domain.Models
{
    public class Supply
    {
        private Guid Id;
        private Guid SupplierId;
        private DateOnly SupplyDate;
        private decimal TotalAmount;

        public Supply(Guid supplierId, DateOnly supplyDate, decimal totalAmount)
        {
            if (supplyDate > DateOnly.FromDateTime(DateTime.Now)) throw new ArgumentException("Supply date add can't be in the future", nameof(supplyDate));
            if (supplyDate == default) throw new ArgumentException("Supply date can't be default", nameof(supplyDate));

            if (totalAmount <= 0) throw new ArgumentException("Supply total amount can't be zero or less", nameof(totalAmount));

            SupplierId = supplierId;
            SupplyDate = supplyDate;
            TotalAmount = totalAmount;
        }
        public Supply(Guid id, Guid supplierId, DateOnly supplyDate, decimal totalAmount)
                     : this(supplierId, supplyDate, totalAmount)
        {
            Id = id;
        }

        public Guid GetId() { return  Id; }
        public Guid GetSupplierId() { return SupplierId; }
        public DateOnly GetSupplyDate() { return SupplyDate; }
        public decimal GetTotalAmount() { return TotalAmount; }
    }
}
