using System.ComponentModel.DataAnnotations;

namespace Blog_Recetas.Models
{
    public class Publicacion
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Titulo:")]
        [Required(ErrorMessage = "Por favor ingresar el Nombre del Titulo:")]
        [StringLength(150, MinimumLength = 4)]
        public string Titulo { get; set; } = string.Empty;

        [Display(Name = "Subtitulo:")]
        [Required(ErrorMessage = "Por favor ingresar el Nombre del Subtitulo:")]
        public string Subtitulo { get; set; } = string.Empty;

        public int AutorId { get; set; }

        [Display(Name = "Autor:")]
        [Required(ErrorMessage = "Por favor ingresar el Autor:")]
        public Autor Autor { get; set; }

        [Display(Name = "Descripción:")]
        [Required(ErrorMessage = "Por favor ingresar la Descripción:")]
        public string Descripcion { get; set; } = string.Empty;

        [Display(Name = "Pie De Pagina:")]
        [Required(ErrorMessage = "Por favor ingresar el Pie de Pagina:")]
        [StringLength(250, MinimumLength = 4)]
        public string PieDePagina { get; set; } = string.Empty;

        [Display(Name = "Fecha De Publicacion")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Por favor ingresar Fecha De Publicacion:")]
        public DateTime FechaPublicacion { get; set; }



        [Required(ErrorMessage = "Por favor selecciona una imagen para la Publicación.")]
        [Display(Name = "Imagen de Publicacíon")]
        public string ImagenUrl { get; set; } = string.Empty;

        [Display(Name = "Tiempo De Preparacion:")]
        [Required(ErrorMessage = "Por favor ingresar el Tiempo De Preparacion:")]
        [StringLength(150, MinimumLength = 4)]
        public string TiempoPreparacion { get; set; } = string.Empty; // en minutos
        [Display(Name = "Tiempo De Coccion:")]
        [Required(ErrorMessage = "Por favor ingresar el Tiempo De Coccion:")]
        [StringLength(150, MinimumLength = 4)]
        public string TiempoCoccion { get; set; } = string.Empty; // en minutos

        [Display(Name = "Porciones:")]
        [Required(ErrorMessage = "Por favor ingresar las Porciones:")]

        public int Porciones { get; set; }

        public int CategoriaId { get; set; }

        [Display(Name = "Categoria:")]
        [Required(ErrorMessage = "Por favor ingresar la Categoria:")]
        public Categoria Categoria { get; set; }

        [Display(Name = "Calorias:")]
        [Required(ErrorMessage = "Por favor ingresar las Calorias:")]

        public int Calorias { get; set; }
        public ICollection<Ingrediente> Ingredientes { get; set; }
        public ICollection<Instruccion> Instrucciones { get; set; }
    }
}
