using Blog_Recetas.Data;
using Blog_Recetas.Models;
using Blog_Recetas.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Blog_Recetas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IRepositoryPublicacion _repositoryPublicacion;
        private readonly BlogContext _context;
        //private readonly 
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IRepositoryPublicacion repositoryPublicacion, BlogContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _repositoryPublicacion = repositoryPublicacion;
            _context = context;
        }


        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {

            var publicacions = await _repositoryPublicacion.GetAll();
            return View(publicacions);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Receta(string Filtro, int CategoriaId)
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre");
            var blogContext = await _repositoryPublicacion.PublicacionFiltro(Filtro, CategoriaId);

            return View(blogContext);
        }
        [AllowAnonymous]


        // GET: Publicacion/Details/5
        public async Task<IActionResult> Post(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacion = await _repositoryPublicacion.GetId((int)id);
            if (publicacion == null)
            {
                return NotFound();
            }

            return View(publicacion);
        }
        [Authorize]
        public async Task<IActionResult> PostRecomendado()
        {

            var publicaion = await _repositoryPublicacion.ObtenerPublicacionMasReciente();
            return View(publicaion);
        }
        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Contact()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Contact(Contacto contacto)
        {
            if (!ModelState.IsValid)
            {
                return View("Contact", contacto);
            }
            var rule = new ContactoRules(_configuration);

            var mensaje = @"
                            <h1>Gracias por contactarse</h1>
                            <p>Hemos recibido tu correo exitosamente.</p>
                            <p>A la brevedad nos pondremos en contacto.</p>
                            <hr/>
                            <p>Saludos,</p>
                            <p><b>Blog De Recetas</b></p>";

            rule.SendEmail(contacto.Email, mensaje, "Mensaje Recibido", "Blog De Recetas", "BlogDeRecetas@gmail.com.ar");
            rule.SendEmail("banegazfacundo@gmail.com", contacto.Message, "Mensaje Recibido", contacto.Name, contacto.Email);


            return RedirectToAction("Contact");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
