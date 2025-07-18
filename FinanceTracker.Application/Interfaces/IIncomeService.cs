using FinanceTracker.Application.DTOs.Income;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Application.Interfaces
{
    public interface IIncomeService
    {
        Task<IEnumerable<IncomeDto>> GetAllAsync(Guid userId);
        Task<IncomeDto?> GetByIdAsync(Guid id, Guid userId);
        Task<IncomeDto> CreateAsync(CreateIncomeDto dto, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
        Task UpdateAsync(int id, UpdateIncomeDto incomeDto);

    }

}
