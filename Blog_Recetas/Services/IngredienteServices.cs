using Blog_Recetas.Data;
using Blog_Recetas.Models;
using Blog_Recetas.Repository;
using Microsoft.EntityFrameworkCore;

namespace Blog_Recetas.Services
{
    public class IngredienteServices : IRepositoryIngrediente
    {
        private readonly BlogContext _context;

        public IngredienteServices(BlogContext context)
        {
            _context = context;
        }


        public async Task AddAIngrediente(Ingrediente ingrediente)
        {
            _context.Ingredientes.Add(ingrediente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIngrediente(int id)
        {
            var ingrediente = await _context.Ingredientes.FirstOrDefaultAsync(m => m.Id == id);

            if (ingrediente == null)
            {
                // Maneja el caso cuando no se encuentra el autor
                throw new ArgumentException("El Ingrediente no fue encontrado.");
            }

            _context.Ingredientes.Remove(ingrediente);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Ingrediente>> GetAll()
        {
            return await _context.Ingredientes.ToListAsync();
        }

        public async  Task<Ingrediente> GetId(int id)
        {
            var ingrediente = await _context.Ingredientes.Include(i => i.Publicacion).FirstOrDefaultAsync(m => m.Id == id);

            if (ingrediente == null)
            {
                throw new ArgumentException("No se encontró un autor con el ID especificado.");
            }
            return ingrediente;
        }

        public async Task UpdateIngrediente(Ingrediente ingrediente)
        {
            _context.Ingredientes.Update(ingrediente);
            await _context.SaveChangesAsync();
        }
    }
}
