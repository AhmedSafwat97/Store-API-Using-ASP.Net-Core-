using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talabat.API.Dtos
{
    public class AddProductDto
    {   

        public string Name { get; set; }

        public string Description { get; set; }

        // We Mus Install Microsoft.AspNetCore.Http
        public IFormFile Picture { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        public int CategoryId { get; set; }

    }
}
