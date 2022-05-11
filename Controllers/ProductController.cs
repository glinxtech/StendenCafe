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
        private readonly ProductRepository productRepo;

        public ProductController(ProductRepository productrepo)
        {
            productRepo = productrepo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            return await productRepo.Get(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await productRepo.Get();

            return products.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Add(Product product)
        {
            return await productRepo.Add(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var productToRem = await productRepo.Get(id);

            if (productToRem == null)
                return BadRequest();

            await productRepo.Delete(id);

            return Ok();
        }
    }
}
