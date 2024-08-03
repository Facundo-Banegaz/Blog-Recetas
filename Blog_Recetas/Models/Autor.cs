using System.ComponentModel.DataAnnotations;

namespace Blog_Recetas.Models
{
    public class Autor
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nombre:")]
        [Required(ErrorMessage = "Por favor ingresar el Nombre:")]
        [StringLength(150, MinimumLength = 4)]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Segundo Nombre:")]
        [Required(ErrorMessage = "Por favor ingresar el Segundo Nombre:")]
        [StringLength(150, MinimumLength = 4)]

        public string SegundoNombre { get; set; } = string.Empty;

        [Display(Name = "Apellido:")]
        [Required(ErrorMessage = "Por favor ingresar el Apellido:")]
        [StringLength(150, MinimumLength = 4)]
        public string Apellido { get; set; } = string.Empty;

        [Display(Name = "Biografia:")]
        [Required(ErrorMessage = "Por favor ingresar la Biografia:")]
        public string Biografia { get; set; } = string.Empty;


        [Display(Name = "Descripcíon:")]
        [Required(ErrorMessage = "Por favor ingresar la Descripcíon:")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Por favor selecciona una imagen.")]
        [Display(Name = "Imagen")]
        public string FotoUrl { get; set; } = string.Empty;

        // Relación uno a muchos con Publicacion
        public ICollection<Publicacion> Publicaciones { get; set; }
    }
}
