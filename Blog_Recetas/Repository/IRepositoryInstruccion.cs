using Blog_Recetas.Models;

namespace Blog_Recetas.Repository
{
    public interface IRepositoryInstruccion
    {
        Task<IEnumerable<Instruccion>> GetAll();
        Task<Instruccion> GetId(int id);
        Task AddInstruccion(Instruccion instruccion);
        Task UpdateInstruccion(Instruccion instruccion);
        Task DeleteInstruccion(int id);
    }
}
