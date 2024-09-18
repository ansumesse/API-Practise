using Microsoft.EntityFrameworkCore;
using Practise.Models;

namespace Practise.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
