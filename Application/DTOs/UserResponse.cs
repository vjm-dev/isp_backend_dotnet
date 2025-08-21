namespace ISP_Backend_Dotnet.Application.DTOs
{
    public class UserResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        public decimal MonthlyPayment { get; set; }
        public DataUsageResponse? DataUsage { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class DataUsageResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Used { get; set; }
        public decimal Limit { get; set; }
        public List<DataConsumptionResponse> DailyUsage { get; set; } = new List<DataConsumptionResponse>();
    }

    public class DataConsumptionResponse
    {
        public DateTime Date { get; set; }
        public decimal Download { get; set; }
        public decimal Upload { get; set; }
    }
}