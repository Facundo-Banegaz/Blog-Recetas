using System.ComponentModel.DataAnnotations;

namespace Blog_Recetas.Models
{
    public class Ingrediente
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nombre:")]
        [Required(ErrorMessage = "Por favor ingresar el Nombre:")]
        [StringLength(150, MinimumLength = 3)]
        public string Nombre { get; set; } = string.Empty;
        [Display(Name = "Cantidad:")]

        [Required(ErrorMessage = "Por favor ingresar  la Cantidad:")]
        public string Cantidad { get; set; } = string.Empty; // Ejemplo: "2 tazas", "1 cucharada"

        [Display(Name = "Unidad:")]
        [Required(ErrorMessage = "Por favor ingresar la Unidad:")]
        public string Unidad { get; set; } = string.Empty; // Ejemplo: "taza", "cucharada", "gramos"

        [Display(Name = "Descripción:")]
        [Required(ErrorMessage = "Por favor ingresar la Descripción:")]
        public string Descripcion { get; set; } = string.Empty; // Detalles adicionales, por ejemplo, "cortadas en cubos"

        // Relación muchos a uno con Publicacion
        public int PublicacionId { get; set; }

        [Display(Name = "Publicacíon:")]
        [Required(ErrorMessage = "Por favor ingresar la  Publicacíon:")]
        public Publicacion Publicacion { get; set; }
    }
}
