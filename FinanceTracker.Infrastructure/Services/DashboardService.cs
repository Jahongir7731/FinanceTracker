using FinanceTracker.Application.DTOs.TrendDto;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Enums;
using FinanceTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;
        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TrendDto> GetTrendAsync(Guid userId)
        {
            var now = DateTime.UtcNow;
            var thisMonth = new DateTime(now.Year, now.Month, 1);
            var lastMonth = thisMonth.AddMonths(-1);

            var thisMonthExpense = await _context.Transactions
                .Where(x => x.UserId == userId && x.Type == TransactionType.Expense && x.CreatedAt >= thisMonth && !x.IsDeleted)
                .SumAsync(x => x.Amount);

            var lastMonthExpense = await _context.Transactions
                .Where(x => x.UserId == userId && x.Type == TransactionType.Expense && x.CreatedAt >= lastMonth && x.CreatedAt < thisMonth && !x.IsDeleted)
                .SumAsync(x => x.Amount);

            return new TrendDto
            {
                ThisMonth = thisMonthExpense,
                LastMonth = lastMonthExpense,
                Trend = thisMonthExpense - lastMonthExpense
            };
        }
    }
}
