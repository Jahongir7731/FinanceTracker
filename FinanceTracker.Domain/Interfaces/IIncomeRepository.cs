using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Domain.Interfaces
{
    public interface IIncomeRepository
    {
        Task<IEnumerable<Income>> GetAllByUserIdAsync(Guid userId);
        Task<Income?> GetByIdAsync(Guid id, Guid userId);
        Task<Income> AddAsync(Income income);
        Task<bool> DeleteAsync(Guid id, Guid userId);
        Task<Income?> GetByIdAsync(int id);
        Task<Income> CreateAsync(Income income, Guid userId);
        Task SaveChangesAsync();
        Task UpdateAsync(Income income);
    }

}
