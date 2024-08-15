using Blog_Recetas.Models;
using Blog_Recetas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog_Recetas.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AutorController : Controller
    {
        private readonly IRepositoryAutor _autorServices;
        private readonly IWebHostEnvironment _env;

        public AutorController(IRepositoryAutor autorServices, IWebHostEnvironment env)
        {

            _autorServices = autorServices;
            _env = env;
        }

        // GET: Autor
        public async Task<IActionResult> Index()
        {

            var autores = await  _autorServices.GetAll();

            return View(autores);
        }


        // GET: Autor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var autor = await _autorServices.GetId((int)id);
                return View(autor);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: Autor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre, SegundoNombre, Apellido, Descripcion, FotoUrl, Biografia")] Autor autor)
        {


            var file = Request.Form.Files["FotoUrl"];

            string NombreCarpeta = "assets/img/autores";

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
                    return View(autor);
                }
            }
            if (file != null && file.Length > 0)
            {
                string NombreArchivo = file.FileName;
                string RutaFullCompleta = Path.Combine(RutaCompleta, NombreArchivo);

                autor.FotoUrl = Path.Combine(NombreCarpeta, NombreArchivo);

                try
                {
                    using (var stream = new FileStream(RutaFullCompleta, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error guardando el archivo: " + ex.Message);
                    return View(autor);
                }
            }


                await _autorServices.AddAutor(autor);
                return RedirectToAction(nameof(Index));
 

        }

        // GET: Autor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var autor = await _autorServices.GetId((int)id);
                return View(autor);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: Autor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,SegundoNombre,Apellido,Biografia,FotoUrl")] Autor autor)
        {
            if (id != autor.Id)
            {
                return NotFound();
            }

            var autorExistente = await _autorServices.GetId(id);

            if (autorExistente == null)
            {
                return NotFound();
            }


                var file = Request.Form.Files["FotoUrl"];

                if (file != null && file.Length > 0)
                {
                    string NombreCarpeta = "assets/img/autores";
                    string RutaRaiz = _env.WebRootPath;
                    string RutaCompleta = Path.Combine(RutaRaiz, NombreCarpeta);
                    string NombreArchivo = file.FileName;
                    string RutaFullCompleta = Path.Combine(RutaCompleta, NombreArchivo);

                    // Crear la carpeta si no existe
                    if (!Directory.Exists(RutaCompleta))
                    {
                        Directory.CreateDirectory(RutaCompleta);
                    }

                    // Eliminar la imagen antigua si existe
                    if (!string.IsNullOrEmpty(autorExistente.FotoUrl))
                    {
                        string RutaArchivoExistente = Path.Combine(RutaRaiz, autorExistente.FotoUrl);
                        if (System.IO.File.Exists(RutaArchivoExistente))
                        {
                            System.IO.File.Delete(RutaArchivoExistente);
                        }
                    }

                    // Actualizar la URL de la nueva imagen
                    autor.FotoUrl = Path.Combine(NombreCarpeta, NombreArchivo);

                    // Guardar la nueva imagen
                    using (var stream = new FileStream(RutaFullCompleta, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                else
                {
                    // Mantener la imagen existente si no se sube una nueva
                    autor.FotoUrl = autorExistente.FotoUrl;
                }

                try
                {
                    // Actualizar las propiedades del autor existente
                    autorExistente.Nombre = autor.Nombre;
                    autorExistente.SegundoNombre = autor.SegundoNombre;
                    autorExistente.Apellido = autor.Apellido;
                    autorExistente.Biografia = autor.Biografia;
                    autorExistente.FotoUrl = autor.FotoUrl;

                    await _autorServices.UpdateAutor(autorExistente);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));

        }




        // GET: Autor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var autor = await _autorServices.GetId((int)id);
                return View(autor);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: Autor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _autorServices.DeleteAutor(id);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        private bool AutorExists(int id)
        {
            return _autorServices.GetId(id) != null;
        }
    }
}
