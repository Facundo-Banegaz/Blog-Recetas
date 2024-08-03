using Blog_Recetas.Models;

namespace Blog_Recetas.Services
{
    public interface IRepositoryAutor
    {
        Task<IEnumerable<Autor>> GetAll();
        Task<Autor> GetId(int id);
        Task AddAutor(Autor autor);
        Task UpdateAutor(Autor autor);
        Task DeleteAutor(int id);
    }
}
