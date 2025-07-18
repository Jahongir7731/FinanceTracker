using FinanceTracker.Application.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync(Guid userId);
        Task<CategoryDto> GetByIdAsync(Guid userId, Guid id);
        Task<CategoryDto> CreateAsync(Guid userId, CreateCategoryDto dto);
        Task<CategoryDto> UpdateAsync(Guid userId, UpdateCategoryDto dto);
        Task DeleteAsync(Guid userId, Guid id);
    }
}
