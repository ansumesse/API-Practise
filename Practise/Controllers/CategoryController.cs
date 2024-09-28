using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practise.Repository;

namespace Practise.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo categoryRepo;

        public CategoryController(ICategoryRepo categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(categoryRepo.Get());
        }
        
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            return Ok(categoryRepo.GetById(id));
        }
    }
}
