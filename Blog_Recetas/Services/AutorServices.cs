using Blog_Recetas.Data;
using Blog_Recetas.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Blog_Recetas.Services
{
    public class AutorServices : IAutorRepository
    {
        private readonly BlogContext _blogContext;

        public AutorServices(BlogContext blogContext)
        {

            _blogContext = blogContext;
        }
        public async Task AddAutor( Autor autor)
        {

            _blogContext.Add(autor);
            await _blogContext.SaveChangesAsync();
        }

        public async Task DeleteAutor(int id)
        {
            await _blogContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Autor>> GetAll()
        {
           var autores  =await _blogContext.Autores.ToListAsync();

            return autores;
        }

        public async Task<Autor> GetId(int id)
        {
                var autor = await _blogContext.Autores
                   .FirstOrDefaultAsync(m => m.Id == id);   

            return autor;
        }

        public async Task UpdateAutor(Autor autor)
        {
            _blogContext.Update(autor);
            await _blogContext.SaveChangesAsync();
        }
    }
}
