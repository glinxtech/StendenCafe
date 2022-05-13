namespace StendenCafe.Models
{
    public class ProductsDTO
    {
        public ProductsDTO()
        {
            Products = new List<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
