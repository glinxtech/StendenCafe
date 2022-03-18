using Microsoft.AspNetCore.Mvc;
using StendenCafe.Services;
using StendenCafe.Models;

namespace StendenCafe.Controllers
{
    public class CategoryController : MyControllerBase
    {
        private readonly CategoryRepository categoryRepo;

        public CategoryController(CategoryRepository categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            return await categoryRepo.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Add(Category category)
        {
            return await categoryRepo.Add(category);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var productToRem = await categoryRepo.Get(id);

            if (productToRem == null)
                return BadRequest();

            await categoryRepo.Delete(id);

            return Ok();
        }
    }
}
