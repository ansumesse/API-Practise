using Microsoft.EntityFrameworkCore.ChangeTracking;
using Practise.Data;
using Practise.Models;

namespace Practise.Repository
{
    public class ProductRepo : IProductRepo
    {
        private readonly ApplicationDbContext context;

        public ProductRepo(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Product> Get()
        {
            return context.Products.ToList();
        }

        public Product GetById(int id)
        {
            return context.Products.Find(id);
        }

        public void New(Product newProduct)
        {
            context.Add(newProduct);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Remove(GetById);
            context.SaveChanges();
        }

        public void Update(int id, Product product)
        {
            Product oldProduct = GetById(id);
            oldProduct.Name = product.Name;
            oldProduct.Price = product.Price;
            oldProduct.Description = product.Description;
            context.SaveChanges();
        }
    }
}
