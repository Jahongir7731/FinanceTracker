using FinanceTracker.Application.DTOs.Income;
using FinanceTracker.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IncomeController(IIncomeService incomeService, IHttpContextAccessor httpContextAccessor)
        {
            _incomeService = incomeService;
            _httpContextAccessor = httpContextAccessor;
        }

        private Guid GetUserId() =>
            Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _incomeService.GetAllAsync(GetUserId());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _incomeService.GetByIdAsync(id, GetUserId());
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIncomeDto dto)
        {
            var result = await _incomeService.CreateAsync(dto, GetUserId());
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _incomeService.DeleteAsync(id, GetUserId());
            return success ? NoContent() : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateIncomeDto dto)
        {
            await _incomeService.UpdateAsync(id, dto);
            return NoContent();
        }
    }
}
