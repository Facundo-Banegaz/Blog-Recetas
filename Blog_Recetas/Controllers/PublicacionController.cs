using Blog_Recetas.Data;
using Blog_Recetas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Blog_Recetas.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PublicacionController : Controller
    {
        private readonly BlogContext _context;

        public PublicacionController(BlogContext context)
        {
            _context = context;
        }

        // GET: Publicacion
        public async Task<IActionResult> Index()
        {
            var blogContext = _context.Publicaciones.Include(p => p.Autor).Include(p => p.Categoria);
            return View(await blogContext.ToListAsync());
        }

        // GET: Publicacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacion = await _context.Publicaciones
                .Include(p => p.Autor)
                .Include(p => p.Categoria)
                .Include(p => p.Ingredientes)
                .Include(p => p.Instrucciones)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicacion == null)
            {
                return NotFound();
            }

            return View(publicacion);
        }

        // GET: Publicacion/Create
        public IActionResult Create()
        {
            ViewData["AutorId"] = new SelectList(_context.Autores, "Id", "Apellido");
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre");
            return View();
        }

        // POST: Publicacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Subtitulo,AutorId,Descripcion,PieDePagina,FechaPublicacion,ImagenUrlPortada,ImagenUrl,TiempoPreparacion,TiempoCoccion,Porciones,CategoriaId,Calorias")] Publicacion publicacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publicacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorId"] = new SelectList(_context.Autores, "Id", "Apellido", publicacion.AutorId);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", publicacion.CategoriaId);
            return View(publicacion);
        }

        // GET: Publicacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacion = await _context.Publicaciones.FindAsync(id);
            if (publicacion == null)
            {
                return NotFound();
            }
            ViewData["AutorId"] = new SelectList(_context.Autores, "Id", "Apellido", publicacion.AutorId);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", publicacion.CategoriaId);
            return View(publicacion);
        }

        // POST: Publicacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Subtitulo,AutorId,Descripcion,PieDePagina,FechaPublicacion,ImagenUrlPortada,ImagenUrl,TiempoPreparacion,TiempoCoccion,Porciones,CategoriaId,Calorias")] Publicacion publicacion)
        {
            if (id != publicacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publicacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicacionExists(publicacion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorId"] = new SelectList(_context.Autores, "Id", "Apellido", publicacion.AutorId);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", publicacion.CategoriaId);
            return View(publicacion);
        }

        // GET: Publicacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacion = await _context.Publicaciones
                .Include(p => p.Autor)
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var publicacion = await _context.Publicaciones.FindAsync(id);
            if (publicacion != null)
            {
                _context.Publicaciones.Remove(publicacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicacionExists(int id)
        {
            return _context.Publicaciones.Any(e => e.Id == id);
        }
    }
}
