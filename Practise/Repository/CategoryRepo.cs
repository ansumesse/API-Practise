using Microsoft.EntityFrameworkCore;
using Practise.Data;
using Practise.DTO;
using Practise.Models;

namespace Practise.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ApplicationDbContext context;

        public CategoryRepo(ApplicationDbContext context)
        {
            this.context = context;
        }
        public List<Category> Get()
        {
            return context.Categories
                .AsNoTracking()
                .ToList();
        }

        public CategoryDTO GetById(int id)
        {
            Category category = context.Categories
                .AsNoTracking()
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Id == id);
            return  new CategoryDTO()
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products.Select(p => new ProductWithIdAndNameDTO { Id = p.Id, Name = p.Name }).ToList()
            };
        }

    }
}
