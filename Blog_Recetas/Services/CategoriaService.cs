using Blog_Recetas.Data;
using Blog_Recetas.Models;
using Blog_Recetas.Repository;
using Microsoft.EntityFrameworkCore;

namespace Blog_Recetas.Services
{
    public class CategoriaService : IRepositoryCategoria
    {
        private readonly BlogContext _blogContext;

        public CategoriaService(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }



        public async Task AddCategoria(Categoria categoria)
        {
            _blogContext.Categorias.Add(categoria);
            await _blogContext.SaveChangesAsync();
        }

        public async Task DeleteCategoria(int id)
        {
            var categoria = await _blogContext.Categorias.FirstOrDefaultAsync(a => a.Id == id);

            if (categoria == null)
            {
                // Maneja el caso cuando no se encuentra el autor
                throw new ArgumentException("El autor no fue encontrado.");
            }

            _blogContext.Categorias.Remove(categoria);
            await _blogContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            return await _blogContext.Categorias.ToListAsync();
        }

        public async Task<Categoria> GetId(int id)
        {
            var categoria = await _blogContext.Categorias.FirstOrDefaultAsync(a => a.Id.Equals(id));

            if (categoria == null)
            {
                throw new ArgumentException("No se encontró un autor con el ID especificado.");
            }
            return categoria;
        }

        public async Task UpdateCategoria(Categoria categoria)
        {

            _blogContext.Categorias.Update(categoria);
            await _blogContext.SaveChangesAsync();
        }
    }
}
