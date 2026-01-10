namespace microservices.AnalizeAPI.API.Contracts.Responses
{
    public record OrderAnalysisResponse
    (
        DateTime Date,
        decimal TotalAmount,
        int OrderCount,
        decimal AvgOrderAmount
    );
}
