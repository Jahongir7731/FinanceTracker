using AutoMapper;
using FinanceTracker.Application.DTOs.Income;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Interfaces;

namespace FinanceTracker.Infrastructure.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IMapper _mapper;
        private readonly IIncomeRepository _incomeRepository;
        private readonly ICacheService _cache;
        public IncomeService(
            IMapper mapper,
            IIncomeRepository incomeRepository,
            ICacheService cache)
        {
            _mapper = mapper;
            _incomeRepository = incomeRepository;
            _cache = cache;
        }

        public async Task<IEnumerable<IncomeDto>> GetAllCasheAsync(Guid userId)
        {
            var cacheKey = $"incomes-{userId}";
            var cached = await _cache.GetAsync<IEnumerable<IncomeDto>>(cacheKey);

            if (cached is not null)
                return cached;

            var incomes = await _incomeRepository.GetAllByUserIdAsync(userId);
            var mapped = _mapper.Map<IEnumerable<IncomeDto>>(incomes);

            await _cache.SetAsync(cacheKey, mapped, TimeSpan.FromMinutes(10));
            return mapped;
        }



        public async Task<IEnumerable<IncomeDto>> GetAllAsync(Guid userId)
        {
            var incomes = await _incomeRepository.GetAllByUserIdAsync(userId);
            return _mapper.Map<List<IncomeDto>>(incomes);
        }

        public async Task<IncomeDto?> GetByIdAsync(Guid id, Guid userId)
        {
            var income = await _incomeRepository.GetByIdAsync(id, userId);
            return income == null ? null : _mapper.Map<IncomeDto>(income);
        }

        public async Task<IncomeDto> CreateAsync(CreateIncomeDto dto, Guid userId)
        {
            var income = _mapper.Map<Income>(dto);
            income.UserId = userId;

            var created = await _incomeRepository.AddAsync(income);
            return _mapper.Map<IncomeDto>(created);
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            return await _incomeRepository.DeleteAsync(id, userId);
        }

        public async Task UpdateAsync(int id, UpdateIncomeDto incomeDto)
        {
            var income = await _incomeRepository.GetByIdAsync(id);
            if (income == null)
                throw new Exception("Income topilmadi");

            _mapper.Map(incomeDto, income);
            await _incomeRepository.SaveChangesAsync();
        }

        public async Task<Income> UpdateAsync(Income incom)
        {
            var income = await _incomeRepository.GetByIdAsync(incom.Id, incom.UserId);
            if (income == null)
                throw new Exception("Income topilmadi");

            _mapper.Map(incom, income);
            await _incomeRepository.SaveChangesAsync();
            return income;
        }

        public async Task<Income> CreateIncomeAsync(Income income, Guid userId)
        {
            return await _incomeRepository.CreateAsync(income, userId);
        }

        public async Task<IEnumerable<IncomeDto>> GetAllIncomesAsync(Guid userId)
        {
            var incomes = await _incomeRepository.GetAllByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<IncomeDto>>(incomes);
        }
        public async Task UpdateIncomeAsync(Guid id, UpdateIncomeDto dto, Guid userId)
        {
            var income = await _incomeRepository.GetByIdAsync(id, userId)
                         ?? throw new Exception("Income topilmadi");

            _mapper.Map(dto, income);
            await _incomeRepository.UpdateAsync(income);
        }

        public async Task DeleteIncomeAsync(Guid id, Guid userId)
        {
            var income = await _incomeRepository.GetByIdAsync(id, userId)
                         ?? throw new Exception("Income topilmadi");

            await _incomeRepository.DeleteAsync(id, userId);
        }


    }
}
