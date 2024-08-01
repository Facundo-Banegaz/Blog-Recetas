using System.ComponentModel.DataAnnotations;

namespace Blog_Recetas.Models
{
    public class Instruccion
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Numero de Pasos:")]
        [Required(ErrorMessage = "Por favor ingresar el Numero de pasos:")]
        public string NumeroPasos { get; set; } = string.Empty;

        [Display(Name = "Descripción:")]
        [Required(ErrorMessage = "Por favor ingresar la Descripción:")]
        public string Descripcion { get; set; } = string.Empty;

        [Display(Name = "Tiempo:")]
        [Required(ErrorMessage = "Por favor ingresar el Tiempo:")]
        public string Tiempo { get; set; } = string.Empty; // Tiempo requerido para completar este paso

        [Display(Name = "Notas:")]
        [Required(ErrorMessage = "Por favor ingresar la Notas:")]
        public string Notas { get; set; } = string.Empty; // Cualquier nota adicional o consejo

        // Relación muchos a uno con Publicacion
        public int PublicacionId { get; set; }

        [Display(Name = "Publicaíon:")]
        [Required(ErrorMessage = "Por favor ingresar la Publicacíon:")]
        public Publicacion Publicacion { get; set; }
    }
}
