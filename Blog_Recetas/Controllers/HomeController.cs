using Blog_Recetas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog_Recetas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        //private readonly 
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Post()
        {
            return View();
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

        //[Authorize(Roles = "Administrator")]
        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
