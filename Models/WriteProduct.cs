using System.ComponentModel.DataAnnotations;

namespace StendenCafe.Models
{
    public class WriteProduct
    {
        [Required, MinLength(2), MaxLength(128)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required, Range(0, 99.99)]
        public float Price { get; set; }
    }
}
