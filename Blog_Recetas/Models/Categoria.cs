using System.ComponentModel.DataAnnotations;

namespace Blog_Recetas.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nombre:")]
        [Required(ErrorMessage = "Por favor ingresar el Nombre de la Categoria:")]
        [StringLength(150, MinimumLength = 4)]
        public string Nombre { get; set; } = string.Empty;


        // Relación uno a muchos con Publicacion
        public ICollection<Publicacion> Publicaciones { get; set; } = new List<Publicacion>();
    }
}
