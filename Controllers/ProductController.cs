using Microsoft.AspNetCore.Mvc;
using StendenCafe.Services;
using StendenCafe.Models;
using System.Collections.Generic;
using System.Linq;
using StendenCafe.Authentication;

namespace StendenCafe.Controllers
{
    [Authorize]
    public class ProductController : MyControllerBase
    {
        private readonly ProductRepository ProductRepo;

        public ProductController(ProductRepository productrepo)
        {
            ProductRepo = productrepo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            return await ProductRepo.Get(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await ProductRepo.Get();

            return products.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Add(Product product)
        {
            return await ProductRepo.Add(product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Patch([FromRoute] int id, [FromBody] Product product)
        {
            product.Id = id;

            return await ProductRepo.Update(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var productToRem = await ProductRepo.Get(id);
            if (productToRem == null) return BadRequest();
            await ProductRepo.Delete(id);
            return Ok();
        }
    }
}
