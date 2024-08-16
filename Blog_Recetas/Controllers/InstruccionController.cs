using Blog_Recetas.Data;
using Blog_Recetas.Models;
using Blog_Recetas.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Blog_Recetas.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class InstruccionController : Controller
    {
        private readonly IRepositoryInstruccion _repositoryInstruccion;
        private readonly IRepositoryPublicacion _repositoryPublicacion;

        public InstruccionController(IRepositoryInstruccion repositoryInstruccion, IRepositoryPublicacion repositoryPublicacion)
        {
            _repositoryInstruccion = repositoryInstruccion;
            _repositoryPublicacion = repositoryPublicacion;
        }

        // GET: Instruccion
        public async Task<IActionResult> Index()
        {
            var instruccion = await _repositoryInstruccion.GetAll();
            return View(instruccion);
        }

        // GET: Instruccion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instruccion = await _repositoryInstruccion.GetId((int)id);

            if (instruccion == null)
            {
                return NotFound();
            }

            return View(instruccion);
        }

        // GET: Instruccion/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PublicacionId"] = new SelectList(await _repositoryPublicacion.GetAll(), "Id", "Titulo");
            return View();
        }

        // POST: Instruccion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumeroPasos,Descripcion,Tiempo,Notas,PublicacionId")] Instruccion instruccion)
        {
            await _repositoryInstruccion.AddInstruccion(instruccion);
            return RedirectToAction(nameof(Index));
        }

        // GET: Instruccion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instruccion = await _repositoryInstruccion.GetId((int)id);

            if (instruccion == null)
            {
                return NotFound();
            }
            ViewData["PublicacionId"] = new SelectList(await _repositoryPublicacion.GetAll(), "Id", "Titulo");
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

                try
                {
                await _repositoryInstruccion.UpdateInstruccion(instruccion);
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

        // GET: Instruccion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instruccion = await _repositoryInstruccion.GetId((int)id);
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
            try
            {
                await _repositoryInstruccion.DeleteInstruccion(id);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        private bool InstruccionExists(int id)
        {
            return _repositoryInstruccion.GetId(id) != null;
        }
    }
}
