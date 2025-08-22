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

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                PlanName = user.Plan?.Name ?? "",
                MonthlyPayment = user.Plan?.MonthlyPayment ?? 0,
                LastUpdated = user.LastUpdated,
                DataUsage = user.DataUsage != null ?
                    DataUsageToDataUsageResponse(user.DataUsage) :
                    new DataUsageResponse
                    {
                        StartDate = DateTime.UtcNow,
                        EndDate = DateTime.UtcNow.AddMonths(1),
                        Used = 0,
                        Limit = user.Plan?.DataLimit ?? 0,
                        DailyUsage = new List<DataConsumptionResponse>()
                    }
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