using Blog_Recetas.Models;

namespace Blog_Recetas.Repository
{
    public interface IPublicacionRepository
    {
        Task<IEnumerable<Publicacion>> GetAll();
        Task<Publicacion> GetId(int id);
        Task AddPublicacion(Publicacion publicacion);
        Task UpdatePublicacion(Publicacion publicacion);
        Task DeletePublicacion(int id);
    }
}
