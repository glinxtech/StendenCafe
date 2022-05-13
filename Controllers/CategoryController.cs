using Microsoft.AspNetCore.Mvc;
using StendenCafe.Services;
using StendenCafe.Models;

namespace StendenCafe.Controllers
{
    public class CategoryController : MyControllerBase
    {
        private readonly CategoryRepository CategoryRepo;

        public CategoryController(CategoryRepository categoryRepo)
        {
            CategoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            var categories = await CategoryRepo.Get();

            return categories.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            return await CategoryRepo.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Add(Category category)
        {
            return await CategoryRepo.Add(category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> Patch([FromRoute] int id, [FromBody] Category category)
        {
            category.Id = id;

            return await CategoryRepo.Update(category);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var productToRem = await CategoryRepo.Get(id);
            if (productToRem == null) return BadRequest();
            await CategoryRepo.Delete(id);
            return Ok();
        }
    }
}
