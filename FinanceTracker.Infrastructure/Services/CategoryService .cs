using FinanceTracker.Application.DTOs.Category;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _db;
        public CategoryService(AppDbContext db) => _db = db;

        public async Task<IEnumerable<CategoryDto>> GetAllAsync(Guid userId)
        {
            return await _db.Categories
                            .Where(c => c.UserId == userId && !c.IsDeleted)
                            .Select(c => new CategoryDto { Id = c.Id, Name = c.Name, Color = c.Color })
                            .ToListAsync();
        }
          

        public async Task<CategoryDto> GetByIdAsync(Guid userId, Guid id)
        {
            var c = await _db.Categories.FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);
            return new CategoryDto { Id = c.Id, Name = c.Name, Color = c.Color };
        }

        public async Task<CategoryDto> CreateAsync(Guid userId, CreateCategoryDto dto)
        {
            var c = new Category { Id = Guid.NewGuid(), Name = dto.Name, Color = dto.Color, UserId = userId };
            _db.Categories.Add(c);
            await _db.SaveChangesAsync();
            return new CategoryDto { Id = c.Id, Name = c.Name, Color = c.Color };
        }

        public async Task<CategoryDto> UpdateAsync(Guid userId, UpdateCategoryDto dto)
        {
            var c = await _db.Categories.FirstOrDefaultAsync(x => x.UserId == userId && x.Id == dto.Id);
            c.Name = dto.Name;
            c.Color = dto.Color;
            await _db.SaveChangesAsync();
            return new CategoryDto { Id = c.Id, Name = c.Name, Color = c.Color };
        }

        public async Task DeleteAsync(Guid userId, Guid id)
        {
            var c = await _db.Categories.FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);
            if (c == null) return;

            c.IsDeleted = true;
            await _db.SaveChangesAsync();
        }


    }

}
