using FinanceTracker.Application.DTOs.TrendDto;

namespace FinanceTracker.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<TrendDto> GetTrendAsync(Guid userId);
    }

}
