using Blog_Recetas.Data;
using Blog_Recetas.Models;
using Blog_Recetas.Repository;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Blog_Recetas.Services
{
    public class PublicacionService : IRepositoryPublicacion
    {
        private readonly BlogContext _blogContext;

        public PublicacionService(BlogContext blogContext)
        {

            _blogContext = blogContext;
        }

        public async Task AddPublicacion(Publicacion publicacion)
        {
            _blogContext.Publicaciones.Add(publicacion);
            await _blogContext.SaveChangesAsync();
        }

        public async Task DeletePublicacion(int id)
        {
            var publicacion = await _blogContext.Publicaciones.FirstOrDefaultAsync(a => a.Id == id);


            if (publicacion == null)
            {
                // Maneja el caso cuando no se encuentra el autor
                throw new ArgumentException("El autor no fue encontrado.");
            }

            _blogContext.Publicaciones.Remove(publicacion);
            await _blogContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Publicacion>> GetAll()
        {
            return await _blogContext.Publicaciones.Include(p => p.Autor).ToListAsync();
        }



        public async Task<Publicacion> GetId(int id)
        {
            var publicacion = await _blogContext.Publicaciones
                .Include(p => p.Autor)
                .Include(p => p.Categoria).Include(p => p.Ingredientes).Include(p => p.Instrucciones)
                .FirstOrDefaultAsync(m => m.Id == id);

            return publicacion;
        }

        public async Task<Publicacion> ObtenerPublicacionMasReciente()
        {
            var publicacion = await _blogContext.Publicaciones
            .Include(p => p.Autor)
            .Include(p => p.Categoria)
            .Include(p => p.Ingredientes)
            .Include(p => p.Instrucciones)
            .OrderByDescending(p => p.FechaPublicacion)
            .FirstOrDefaultAsync();

            return publicacion;
        }

        public async Task<List<Publicacion>> PublicacionFiltro(string filtro, int categoriaId)
        {
            IQueryable<Publicacion> query = _blogContext.Publicaciones
               .AsNoTracking()
               .Include(p => p.Autor)
               .Include(p => p.Categoria)
               .Include(p => p.Ingredientes)
               .Include(p => p.Instrucciones);

            if (!string.IsNullOrEmpty(filtro) || categoriaId != 0)
            {
                query = query.Where(p =>
                    (string.IsNullOrEmpty(filtro) || p.Titulo.Contains(filtro) || p.Subtitulo.Contains(filtro)) &&
                    (categoriaId == 0 || p.CategoriaId == categoriaId)
                );
            }

            return await query.ToListAsync();

        }

        public async Task UpdatePublicacion(Publicacion publicacion)
        {
            _blogContext.Publicaciones.Update(publicacion);
            await _blogContext.SaveChangesAsync();
        }
    }
}
