namespace Practise.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductWithIdAndNameDTO> Products { get; set; } = new List<ProductWithIdAndNameDTO>();
    }
}
