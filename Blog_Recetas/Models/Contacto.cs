using System.ComponentModel.DataAnnotations;

namespace Blog_Recetas.Models
{
    public class Contacto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Phone]
        public string? Phone { get; set; } = string.Empty;
        
        
        [Required]
        public string Message { get; set; } = string.Empty;


    }
}
