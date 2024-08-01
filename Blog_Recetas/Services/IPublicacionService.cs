using Blog_Recetas.Models;

namespace Blog_Recetas.Service
{
    public interface IPublicacionService
    {
        Task<IEnumerable<Publicacion>> GetAll();
        Task<Publicacion> GetId(int id);
        Task AddPublicacion(Publicacion publicacion);
        Task UpdatePublicacion(Publicacion publicacion);
        Task DeletePublicacion(int id);
    }
}
