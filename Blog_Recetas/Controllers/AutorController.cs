using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blog_Recetas.Data;
using Blog_Recetas.Models;
using Blog_Recetas.Services;
using Microsoft.AspNetCore.Authorization;

namespace Blog_Recetas.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AutorController : Controller
    {
        private readonly IRepositoryAutor _autorServices;


        public AutorController(IRepositoryAutor autorServices)
        {
            

            this._autorServices = autorServices;
        }

        // GET: Autor
        public async Task<IActionResult> Index()
        {

            var autor = await _autorServices.GetAll();

            return View(autor);
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
            if (ModelState.IsValid)
            {
                
                await _autorServices.AddAutor(autor);
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre, SegundoNombre, Apellido, Descripcion, FotoUrl, Biografia")] Autor autor)
        {
            if (id != autor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    await _autorServices.UpdateAutor(autor);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorExists(autor.Id))
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
            return View(autor);
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
