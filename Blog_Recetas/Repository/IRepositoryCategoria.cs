using Blog_Recetas.Models;

namespace Blog_Recetas.Repository
{
    public interface IRepositoryCategoria
    {
        Task<IEnumerable<Categoria>> GetAll();
        Task<Categoria> GetId(int id);

        Task AddCategoria(Categoria categoria);
        Task UpdateCategoria(Categoria categoria);
        Task DeleteCategoria(int id);
    }
}
