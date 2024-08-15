using Blog_Recetas.Data;
using Blog_Recetas.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog_Recetas.Services
{
    public class AutorServices : IRepositoryAutor
    {
        private readonly BlogContext _blogContext;

        public AutorServices(BlogContext blogContext)
        {

            _blogContext = blogContext;
        }

        public async Task AddAutor(Autor autor)
        {
            _blogContext.Autores.Add(autor);
            await _blogContext.SaveChangesAsync();
        }

        public async Task DeleteAutor(int id)
        {
            var autor = await _blogContext.Autores.FirstOrDefaultAsync(a => a.Id == id);

            if (autor == null)
            {
                // Maneja el caso cuando no se encuentra el autor
                throw new ArgumentException("El autor no fue encontrado.");
            }

            _blogContext.Autores.Remove(autor);
            await _blogContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<Autor>> GetAll()
        {
            return await _blogContext.Autores.ToListAsync();
        }

        public async Task<Autor> GetId(int id)
        {
            var autor = await _blogContext.Autores.FirstOrDefaultAsync(a => a.Id.Equals(id));

            if (autor == null)
            {
                throw new ArgumentException("No se encontró un autor con el ID especificado.");
            }
            return autor;
        }

        public async Task UpdateAutor(Autor autor)
        {

            _blogContext.Autores.Update(autor);
            await _blogContext.SaveChangesAsync();
        }
    }
}
