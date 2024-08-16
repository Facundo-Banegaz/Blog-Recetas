using Blog_Recetas.Data;
using Blog_Recetas.Models;
using Blog_Recetas.Repository;
using Microsoft.EntityFrameworkCore;

namespace Blog_Recetas.Services
{
    public class InstruccionServices : IRepositoryInstruccion
    {
        private readonly BlogContext _blogContext;

        public InstruccionServices(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }


        public async Task AddInstruccion(Instruccion instruccion)
        {
            _blogContext.Instrucciones.Add(instruccion);
            await _blogContext.SaveChangesAsync();
        }

        public async Task DeleteInstruccion(int id)
        {
            var instruccion = await _blogContext.Instrucciones.Include(i => i.Publicacion).FirstOrDefaultAsync(a => a.Id == id);

            if (instruccion == null)
            {
                // Maneja el caso cuando no se encuentra el autor
                throw new ArgumentException("La instruccion no fue encontrado.");
            }

            _blogContext.Instrucciones.Remove(instruccion);

            await _blogContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Instruccion>> GetAll()
        {
            return await _blogContext.Instrucciones.ToListAsync();
        }

        public async Task<Instruccion> GetId(int id)
        {
            var instruccion = await _blogContext.Instrucciones.Include(i => i.Publicacion).FirstOrDefaultAsync(a => a.Id.Equals(id));

            if (instruccion == null)
            {
                throw new ArgumentException("No se encontró una instruccion con el ID especificado.");
            }
            return instruccion;
        }

        public async  Task UpdateInstruccion(Instruccion instruccion)
        {
            _blogContext.Instrucciones.Update(instruccion);
            await _blogContext.SaveChangesAsync();
        }
    }
}
