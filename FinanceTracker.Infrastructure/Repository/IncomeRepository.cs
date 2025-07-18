using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Interfaces;
using FinanceTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Infrastructure.Repository
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly AppDbContext _context;

        public IncomeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Income>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.Incomes
                .Where(i => i.UserId == userId && !i.IsDeleted)
                .Include(i => i.Category)
                .ToListAsync();
        }

        public async Task<Income?> GetByIdAsync(Guid id, Guid userId)
        {
            return await _context.Incomes
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
        }

        public async Task<Income?> GetByIdAsync(int id)
        {
            return await _context.Incomes.FindAsync(id);
        }

        public async Task<Income> AddAsync(Income income)
        {
            _context.Incomes.Add(income);
            await _context.SaveChangesAsync();
            return income;
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var income = await GetByIdAsync(id, userId);
            if (income == null) return false;

            income.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Income> CreateAsync(Income income, Guid userId)
        {
            income.UserId = userId;

            await _context.Incomes.AddAsync(income);
            await _context.SaveChangesAsync();
            return income;
        }
        public async Task UpdateAsync(Income income)
        {
            var existingIncome = await _context.Incomes.FindAsync(income.Id);

            if (existingIncome == null)
            {
                throw new Exception("Income topilmadi bu id da");
            }

            existingIncome.Amount = income.Amount;
            existingIncome.Description = income.Description;
            existingIncome.Date = income.Date;
            existingIncome.CategoryId = income.CategoryId;

            _context.Incomes.Update(existingIncome);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }



    }

}
