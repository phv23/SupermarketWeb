using Business_Logic.DTOs;
using Business_Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.API
{
    [Route("api/category")]
    [ApiController]
    public class CategoryAPI : ControllerBase
    {
        // categoryService là một đối tượng kiểu ICategoryService, được tiêm vào qua Dependency Injection trong constructor của controller
        private readonly ICategoryService categoryService;

        public CategoryAPI(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAll()
        {
            var categories = await categoryService.FindAll();
            return Ok(categories); // Trả về danh sách danh mục
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            var category = await categoryService.FindOne(id);
            if (category.Id == null)
            {
                // Trả về 404
                return NotFound("Không tìm thấy"); 
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Create(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                // Trả về 400
                return BadRequest("Dữ liệu không hợp lệ"); 
            }

            var createdCategory = await categoryService.Save(categoryDTO);
            // Tự động trả về 201 (Created) nhờ method helper của ControllerBase cung cấp
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut]
        public async Task<ActionResult<CategoryDTO>> Update(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null || categoryDTO.Id == null)
            {
                // Trả về 400
                return BadRequest("Dữ liệu không hợp lệ"); 
            }

            var updatedCategory = await categoryService.Update(categoryDTO);
            if (updatedCategory == null)
            {
                // Trả về 404
                return NotFound("Không tìm thấy thông tin cần cập nhật");
            }
            // Trả về 200
            return Ok(updatedCategory); 
        }

    }
}
