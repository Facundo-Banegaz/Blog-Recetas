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

            return autor;
        }

        public async Task UpdateAutor(Autor autor)
        {

            _blogContext.Autores.Update(autor);
            await _blogContext.SaveChangesAsync();
        }
    }
}
