using Practise.Models;

namespace Practise.Repository
{
    public interface IProductRepo
    {
        IEnumerable<Product> Get();
        Product GetById(int id);
        void New(Product newProduct);
        void Update(int id, Product product);
        void Delete(int id);
    }
}
