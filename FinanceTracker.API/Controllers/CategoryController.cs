using FinanceTracker.Application.DTOs.Category;
using FinanceTracker.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceTracker.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        } 

        private Guid UserId => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        [HttpGet]
        public async Task<IActionResult> GetAll()
        { 
            return Ok(await _categoryService.GetAllAsync(UserId));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        { 
            return Ok(await _categoryService.GetByIdAsync(UserId, id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        { 
            return Ok(await _categoryService.CreateAsync(UserId, dto));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryDto dto)
        {
            dto.Id = id;
            return Ok(await _categoryService.UpdateAsync(UserId, dto));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _categoryService.DeleteAsync(UserId, id);
            return NoContent();
        }
    }
}
