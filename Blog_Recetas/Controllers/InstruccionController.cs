using Blog_Recetas.Data;
using Blog_Recetas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Blog_Recetas.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class InstruccionController : Controller
    {
        private readonly BlogContext _context;

        public InstruccionController(BlogContext context)
        {
            _context = context;
        }

        // GET: Instruccion
        public async Task<IActionResult> Index()
        {
            var blogContext = _context.Instrucciones.Include(i => i.Publicacion);
            return View(await blogContext.ToListAsync());
        }

        // GET: Instruccion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instruccion = await _context.Instrucciones
                .Include(i => i.Publicacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instruccion == null)
            {
                return NotFound();
            }

            return View(instruccion);
        }

        // GET: Instruccion/Create
        public IActionResult Create()
        {
            ViewData["PublicacionId"] = new SelectList(_context.Publicaciones, "Id", "Descripcion");
            return View();
        }

        // POST: Instruccion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumeroPasos,Descripcion,Tiempo,Notas,PublicacionId")] Instruccion instruccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instruccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PublicacionId"] = new SelectList(_context.Publicaciones, "Id", "Descripcion", instruccion.PublicacionId);
            return View(instruccion);
        }

        // GET: Instruccion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instruccion = await _context.Instrucciones.FindAsync(id);
            if (instruccion == null)
            {
                return NotFound();
            }
            ViewData["PublicacionId"] = new SelectList(_context.Publicaciones, "Id", "Descripcion", instruccion.PublicacionId);
            return View(instruccion);
        }

        // POST: Instruccion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumeroPasos,Descripcion,Tiempo,Notas,PublicacionId")] Instruccion instruccion)
        {
            if (id != instruccion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instruccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstruccionExists(instruccion.Id))
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
            ViewData["PublicacionId"] = new SelectList(_context.Publicaciones, "Id", "Descripcion", instruccion.PublicacionId);
            return View(instruccion);
        }

        // GET: Instruccion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instruccion = await _context.Instrucciones
                .Include(i => i.Publicacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instruccion == null)
            {
                return NotFound();
            }

            return View(instruccion);
        }

        // POST: Instruccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instruccion = await _context.Instrucciones.FindAsync(id);
            if (instruccion != null)
            {
                _context.Instrucciones.Remove(instruccion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstruccionExists(int id)
        {
            return _context.Instrucciones.Any(e => e.Id == id);
        }
    }
}
