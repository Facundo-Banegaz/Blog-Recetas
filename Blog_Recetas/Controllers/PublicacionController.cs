using Blog_Recetas.Data;
using Blog_Recetas.Models;
using Blog_Recetas.Repository;
using Blog_Recetas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Blog_Recetas.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PublicacionController : Controller
    {
        private readonly IRepositoryPublicacion _repositoryPublicacion;
        private readonly IRepositoryAutor _autorServices;
        private readonly IRepositoryCategoria _categoriaServices;
        private readonly IWebHostEnvironment _env;
        public PublicacionController(IRepositoryPublicacion repositoryPublicacion, 
            IRepositoryCategoria categoriaServices,
            IRepositoryAutor autorServices, IWebHostEnvironment env)
        {
            _repositoryPublicacion = repositoryPublicacion; 
            _categoriaServices = categoriaServices;
            _autorServices = autorServices;
            _env = env;
        }

        // GET: Publicacion
        public async Task<IActionResult> Index()
        {
            var publicacion =await _repositoryPublicacion.GetAll();
            return View(publicacion);
        }

        // GET: Publicacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacion =  await _repositoryPublicacion.GetId((int)id);
            if (publicacion == null)
            {
                return NotFound();
            }

            return View(publicacion);
        }

        // GET: Publicacion/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AutorId"] = new SelectList(await _autorServices.GetAll(), "Id", "Apellido");
            ViewData["CategoriaId"] = new SelectList(await _categoriaServices.GetAll(), "Id", "Nombre");
            return View();
        }

        // POST: Publicacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Subtitulo,AutorId,Descripcion,PieDePagina,FechaPublicacion,ImagenUrl,TiempoPreparacion,TiempoCoccion,Porciones,CategoriaId,Calorias")] Publicacion publicacion)
        {

            var fileImagen = Request.Form.Files["ImagenUrl"];

            string NombreCarpeta = "assets/img/publicaciones";
            string RutaRaiz = _env.WebRootPath;
            string RutaCompleta = Path.Combine(RutaRaiz, NombreCarpeta);



            if (!Directory.Exists(RutaCompleta))
            {
                try
                {
                    Directory.CreateDirectory(RutaCompleta);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error creando la carpeta: " + ex.Message);
                    return View(publicacion);
                }
            }



            if (fileImagen != null && fileImagen.Length > 0)
            {
                string NombreArchivoImagen = fileImagen.FileName;
                string RutaFullCompletaImagen = Path.Combine(RutaCompleta, NombreArchivoImagen);

                publicacion.ImagenUrl = Path.Combine(NombreCarpeta, NombreArchivoImagen);

                try
                {
                    using (var stream = new FileStream(RutaFullCompletaImagen, FileMode.Create))
                    {
                        await fileImagen.CopyToAsync(stream);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error guardando el archivo de imagen: " + ex.Message);
                    return View(publicacion);
                }
            }

            await _repositoryPublicacion.AddPublicacion(publicacion);
            return RedirectToAction(nameof(Index));
        }

        // GET: Publicacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["AutorId"] = new SelectList(await _autorServices.GetAll(), "Id", "Apellido");
            ViewData["CategoriaId"] = new SelectList(await _categoriaServices.GetAll(), "Id", "Nombre");
            return View(publicacion);
        }

        // POST: Publicacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Subtitulo,AutorId,Descripcion,PieDePagina,FechaPublicacion,ImagenUrl,TiempoPreparacion,TiempoCoccion,Porciones,CategoriaId,Calorias")] Publicacion publicacion)
        {
            if (id != publicacion.Id)
            {
                return NotFound();
            }

            var publicacionExistente = await _repositoryPublicacion.GetId(id);

            if (publicacionExistente == null)
            {
                return NotFound();
            }

            var filePortada = Request.Form.Files["ImagenUrlPortada"];
            var fileImagen = Request.Form.Files["ImagenUrl"];

            string NombreCarpeta = "assets/img/publicaciones";
            string RutaRaiz = _env.WebRootPath;
            string RutaCompleta = Path.Combine(RutaRaiz, NombreCarpeta);

            if (!Directory.Exists(RutaCompleta))
            {
                Directory.CreateDirectory(RutaCompleta);
            }



            if (fileImagen != null && fileImagen.Length > 0)
            {
                string NombreArchivoImagen = fileImagen.FileName;
                string RutaFullCompletaImagen = Path.Combine(RutaCompleta, NombreArchivoImagen);

                if (!string.IsNullOrEmpty(publicacionExistente.ImagenUrl))
                {
                    string RutaArchivoExistenteImagen = Path.Combine(RutaRaiz, publicacionExistente.ImagenUrl);
                    if (System.IO.File.Exists(RutaArchivoExistenteImagen))
                    {
                        System.IO.File.Delete(RutaArchivoExistenteImagen);
                    }
                }

                publicacion.ImagenUrl = Path.Combine(NombreCarpeta, NombreArchivoImagen);

                using (var stream = new FileStream(RutaFullCompletaImagen, FileMode.Create))
                {
                    await fileImagen.CopyToAsync(stream);
                }
            }
            else
            {
                publicacion.ImagenUrl = publicacionExistente.ImagenUrl;
            }

            try
            {
                publicacionExistente.Titulo = publicacion.Titulo;
                publicacionExistente.Subtitulo = publicacion.Subtitulo;
                publicacionExistente.AutorId = publicacion.AutorId;
                publicacionExistente.Descripcion = publicacion.Descripcion;
                publicacionExistente.PieDePagina = publicacion.PieDePagina;
                publicacionExistente.FechaPublicacion = publicacion.FechaPublicacion;
                publicacionExistente.ImagenUrl = publicacion.ImagenUrl;
                publicacionExistente.TiempoPreparacion = publicacion.TiempoPreparacion;
                publicacionExistente.TiempoCoccion = publicacion.TiempoCoccion;
                publicacionExistente.Porciones = publicacion.Porciones;
                publicacionExistente.CategoriaId = publicacion.CategoriaId;
                publicacionExistente.Calorias = publicacion.Calorias;

                await _repositoryPublicacion.UpdatePublicacion(publicacionExistente);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: Publicacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Publicacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _repositoryPublicacion.DeletePublicacion(id);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        private bool PublicacionExists(int id)
        {
            return _repositoryPublicacion.GetId(id) != null;
        }
    }
}
