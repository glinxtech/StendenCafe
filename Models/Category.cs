using System.ComponentModel.DataAnnotations;

namespace StendenCafe.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MinLength(2), MaxLength(128)]
        public string? Name { get; set; }
    }
}
