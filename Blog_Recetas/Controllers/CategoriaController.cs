using Blog_Recetas.Data;
using Blog_Recetas.Models;
using Blog_Recetas.Repository;
using Blog_Recetas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog_Recetas.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CategoriaController : Controller
    {
        private readonly IRepositoryCategoria _categoriaServices;

        public CategoriaController(IRepositoryCategoria categoriaServices)
        {
            _categoriaServices = categoriaServices;
        }

        // GET: Categoria
        public async Task<IActionResult> Index()
        {
            var categoria= await _categoriaServices.GetAll();
           
            return View(categoria);
        }

        // GET: Categoria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _categoriaServices.GetId((int)id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categoria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Categoria categoria)
        {
            await _categoriaServices.AddCategoria(categoria);
            return RedirectToAction(nameof(Index));
        }

        // GET: Categoria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var categoria = await _categoriaServices.GetId((int)id);
                return View(categoria);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

        }

        // POST: Categoria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoriaServices.UpdateCategoria(categoria);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.Id))
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
            return View(categoria);
        }

        // GET: Categoria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria =await _categoriaServices.GetId((int)id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _categoriaServices.DeleteCategoria(id);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        private bool CategoriaExists(int id)
        {
            return _categoriaServices.GetId(id) != null;
        }
    }
}
