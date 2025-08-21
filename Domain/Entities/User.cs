namespace ISP_Backend_Dotnet.Domain.Entities
{
    public class User
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int PlanId { get; set; }
        public Plan Plan { get; set; } = null!;
        public DateTime LastUpdated { get; set; }
        public DataUsage? DataUsage { get; set; }
    }

    public class DataUsage
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Used { get; set; }
        public decimal Limit { get; set; }
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }
        public List<DataConsumption> DailyUsage { get; set; } = new List<DataConsumption>();
    }

    public class DataConsumption
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Download { get; set; }
        public decimal Upload { get; set; }
        public int DataUsageId { get; set; }
        public DataUsage? DataUsage { get; set; }
    }
}