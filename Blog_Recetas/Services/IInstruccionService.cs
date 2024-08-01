using Blog_Recetas.Models;

namespace Blog_Recetas.Services
{
    public interface IInstruccionService
    {
        Task<IEnumerable<Instruccion>> GetAll();
        Task<Instruccion> GetId(int id);
        Task AddInstruccion(Instruccion instruccion);
        Task UpdateInstruccion(Instruccion instruccion);
        Task DeleteInstruccion(int id);
    }
}
