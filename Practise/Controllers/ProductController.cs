using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practise.Models;
using Practise.Repository;

namespace Practise.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo productRepo;

        public ProductController(IProductRepo productRepo)
        {
            this.productRepo = productRepo;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(productRepo.Get());
        }

        [HttpGet("{id:int}", Name ="ProductDetailsById")]
        public IActionResult GetById(int id)
        {
            Product product = productRepo.GetById(id);
            if (product is not null)
                return Ok(product);
            return NotFound();
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Product product)
        {
            productRepo.Update(id, product);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            productRepo.Delete(id);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Create(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                productRepo.New(newProduct);
                var url = Url.Link("ProductDetailsById", new {id = newProduct.Id});
                return Created(url, newProduct);
            }
            return BadRequest();
        }
    }
}
