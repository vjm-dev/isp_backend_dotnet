using System.Text.Json.Serialization;

namespace ISP_Backend_Dotnet.Application.DTOs
{
    public class UserResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("phone")]
        public string Phone { get; set; } = string.Empty;

        [JsonPropertyName("planName")]
        public string PlanName { get; set; } = string.Empty;

        [JsonPropertyName("monthlyPayment")]
        public decimal MonthlyPayment { get; set; }

        [JsonPropertyName("data_usage")]
        public DataUsageResponse? DataUsage { get; set; }

        [JsonPropertyName("lastUpdated")]
        public DateTime LastUpdated { get; set; }
    }

    public class DataUsageResponse
    {
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("used")]
        public decimal Used { get; set; }

        [JsonPropertyName("limit")]
        public decimal Limit { get; set; }

        [JsonPropertyName("daily_usage")]
        public List<DataConsumptionResponse> DailyUsage { get; set; } = new List<DataConsumptionResponse>();
    }

    public class DataConsumptionResponse
    {
        public DateTime Date { get; set; }
        public decimal Download { get; set; }
        public decimal Upload { get; set; }
    }
}