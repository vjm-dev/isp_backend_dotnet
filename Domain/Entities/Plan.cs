namespace ISP_Backend_Dotnet.Domain.Entities
{
    public class Plan
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Speed { get; set; } = string.Empty;
        public decimal MonthlyPayment { get; set; }
        public decimal DataLimit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}