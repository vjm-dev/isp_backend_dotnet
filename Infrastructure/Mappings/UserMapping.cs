using Riok.Mapperly.Abstractions;
using ISP_Backend_Dotnet.Domain.Entities;
using ISP_Backend_Dotnet.Application.DTOs;

namespace ISP_Backend_Dotnet.Infrastructure.Mappings
{
    [Mapper]
    public static partial class UserMapping
    {
        public static UserResponse UserToUserResponse(User user)
        {
            if (user == null) return null!;

            DataUsageResponse dataUsageResponse;
            if (user.DataUsage == null)
            {
                dataUsageResponse = new DataUsageResponse
                {
                    StartDate = DateTime.UtcNow.AddDays(-30),
                    EndDate = DateTime.UtcNow.AddDays(5),
                    Used = 0,
                    Limit = user.Plan?.DataLimit ?? 0,
                    DailyUsage = new List<DataConsumptionResponse>()
                };
            }
            else
            {
                dataUsageResponse = DataUsageToDataUsageResponse(user.DataUsage);
            }

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                PlanName = user.Plan?.Name ?? string.Empty,
                MonthlyPayment = user.Plan?.MonthlyPayment ?? 0,
                LastUpdated = user.LastUpdated,
                DataUsage = dataUsageResponse
            };
        }

        public static DataUsageResponse DataUsageToDataUsageResponse(DataUsage dataUsage)
        {
            if (dataUsage == null) return null!;

            return new DataUsageResponse
            {
                StartDate = dataUsage.StartDate,
                EndDate = dataUsage.EndDate,
                Used = dataUsage.Used,
                Limit = dataUsage.Limit,
                DailyUsage = dataUsage.DailyUsage != null ?
                    DataConsumptionListToDataConsumptionResponseList(dataUsage.DailyUsage) :
                    new List<DataConsumptionResponse>()
            };
        }

        [MapperIgnoreSource(nameof(DataConsumption.Id))]
        [MapperIgnoreSource(nameof(DataConsumption.DataUsageId))]
        [MapperIgnoreSource(nameof(DataConsumption.DataUsage))]
        public static partial DataConsumptionResponse DataConsumptionToDataConsumptionResponse(DataConsumption dataConsumption);

        public static partial List<DataConsumptionResponse> DataConsumptionListToDataConsumptionResponseList(List<DataConsumption> dailyUsage);
    }
}