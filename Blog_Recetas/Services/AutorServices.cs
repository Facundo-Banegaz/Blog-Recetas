using Blog_Recetas.Data;
using Blog_Recetas.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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
            _blogContext.Add(autor);
            await _blogContext.SaveChangesAsync();
        }

        public async Task DeleteAutor(int id)
        {
            var autor = await _blogContext.Autores.FindAsync(id);

            if (autor == null)
            {
                throw new KeyNotFoundException($"No se encontró el autor con el ID {id}.");
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
                throw new KeyNotFoundException($"No se encontró el autor con el ID {id}.");
            }

            return autor;
        }

        public async Task UpdateAutor(Autor autor)
        {
            var existingAutor = await _blogContext.Autores.FindAsync(autor.Id);

            if (existingAutor == null)
            {
                throw new KeyNotFoundException($"No se encontró el autor con el ID {autor.Id}.");
            }

            _blogContext.Entry(existingAutor).CurrentValues.SetValues(autor);
            await _blogContext.SaveChangesAsync();
        }
    }
}
