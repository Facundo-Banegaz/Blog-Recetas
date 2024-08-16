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
    public class IngredienteController : Controller
    {
        private readonly IRepositoryIngrediente _repositoryIngrediente;
        private readonly IRepositoryPublicacion _repositoryPublicacion;
        public IngredienteController(IRepositoryIngrediente repositoryIngrediente, IRepositoryPublicacion repositoryPublicacion)
        {
            _repositoryIngrediente = repositoryIngrediente;
            _repositoryPublicacion = repositoryPublicacion;
        }

        // GET: Ingrediente
        public async Task<IActionResult> Index()
        {
            var ingrediente = await _repositoryIngrediente.GetAll();

            return View(ingrediente);
        }


        // GET: Ingrediente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _repositoryIngrediente.GetId((int)id);
            if (ingrediente == null)
            {
                return NotFound();
            }

            return View(ingrediente);
        }

        // GET: Ingrediente/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PublicacionId"] = new SelectList(await _repositoryPublicacion.GetAll(), "Id", "Titulo");
            return View();
        }

        // POST: Ingrediente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Cantidad,Unidad,Descripcion,PublicacionId")] Ingrediente ingrediente)
        {
            await _repositoryIngrediente.AddAIngrediente(ingrediente);
            return RedirectToAction(nameof(Index));
        }

        // GET: Ingrediente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _repositoryIngrediente.GetId((int)id);
            if (ingrediente == null)
            {
                return NotFound();
            }
            ViewData["PublicacionId"] = new SelectList(await _repositoryPublicacion.GetAll(), "Id", "Titulo");
            return View(ingrediente);
        }

        // POST: Ingrediente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Cantidad,Unidad,Descripcion,PublicacionId")] Ingrediente ingrediente)
        {
            if (id != ingrediente.Id)
            {
                return NotFound();
            }

   
                try
                {
                   await  _repositoryIngrediente.UpdateIngrediente(ingrediente);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredienteExists(ingrediente.Id))
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

        // GET: Ingrediente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _repositoryIngrediente.GetId((int)id);
            if (ingrediente == null)
            {
                return NotFound();
            }

            return View(ingrediente);
        }

        // POST: Ingrediente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _repositoryIngrediente.DeleteIngrediente(id);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        private bool IngredienteExists(int id)
        {
            return _repositoryIngrediente.GetId(id) != null;
        }
    }
}
