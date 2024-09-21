using Practise.DTO;
using Practise.Models;

namespace Practise.Repository
{
    public interface ICategoryRepo
    {
        List<Category> Get();
        CategoryDTO GetById(int id);
    }
}
