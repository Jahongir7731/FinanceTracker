using AutoMapper;
using FinanceTracker.Application.DTOs.Income;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Interfaces;
using FinanceTracker.Infrastructure.Services;
using Moq;

namespace FinanceTracker.Tests
{
    public class IncomeServiceTests
    {
        private readonly IncomeService _service;
        private readonly Mock<IIncomeRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ICacheService> _cacheMock;
        public IncomeServiceTests()
        {
            _repoMock = new Mock<IIncomeRepository>();
            _mapperMock = new Mock<IMapper>();
            _cacheMock = new Mock<ICacheService>();

            _service = new IncomeService(_mapperMock.Object, _repoMock.Object, _cacheMock.Object);
        }

        [Fact]
        public async Task CreateAsyncDto()
        {
            var userId = Guid.NewGuid();
            var createDto = new CreateIncomeDto { Amount = 100, Description = "Test" };
            var createdIncome = new Income { Id = Guid.NewGuid(), Amount = 100 };
            var expectedDto = new IncomeDto { Amount = 100 };

            _repoMock.Setup(x => x.CreateAsync(It.IsAny<Income>(), userId))
                     .ReturnsAsync(createdIncome);

            _mapperMock.Setup(x => x.Map<IncomeDto>(createdIncome))
                       .Returns(expectedDto);

            var result = await _service.CreateAsync(createDto, userId);

            Assert.Equal(100, result.Amount);
        }


        [Fact]
        public async Task CreateIncome()
        {
            var userId = Guid.NewGuid();
            var income = new Income
            {
                Id = Guid.NewGuid(),
                Amount = 1000,
                Description = "Test income"
            };

            _repoMock.Setup(r => r.CreateAsync(It.IsAny<Income>(), userId))
                     .ReturnsAsync(income);

            var result = await _service.CreateIncomeAsync(income, userId);

            Assert.Equal(income.Id, result.Id);
            Assert.Equal(income.Amount, result.Amount);
            _repoMock.Verify(r => r.CreateAsync(It.IsAny<Income>(), userId), Times.Once);
        }



        [Fact]
        public async Task GetAllIncomes()
        {
            var userId = Guid.NewGuid();
            var list = new List<Income> { new Income { Id = Guid.NewGuid() } };

            _repoMock.Setup(x => x.GetAllByUserIdAsync(userId)).ReturnsAsync(list);

            var result = await _service.GetAllIncomesAsync(userId);

            Assert.Single(result);
        }

        [Fact]
        public async Task UpdateIncome()
        {
            var userId = Guid.NewGuid();
            var income = new Income { Id = Guid.NewGuid(), Amount = 700 };

            var res = _repoMock.Setup(x => x.UpdateAsync(income));

            var result = await _service.UpdateAsync(income);

            Assert.True(result != null ? true : false);
            _repoMock.Verify(x => x.UpdateAsync(income), Times.Once);
        }

        [Fact]
        public async Task DeleteIncome()
        {
            var userId = Guid.NewGuid();
            var id = Guid.NewGuid();

            _repoMock.Setup(x => x.DeleteAsync(id, userId)).ReturnsAsync(true);

            var result = await _service.DeleteAsync(id, userId);

            Assert.True(result);
        }




    }


}
