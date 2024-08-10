using Blog_Recetas.Models;

namespace Blog_Recetas.Repository
{
    public interface IRepositoryPublicacion
    {
        Task<IEnumerable<Publicacion>> GetAll();
        Task<Publicacion> GetId(int id);
        Task<Publicacion> ObtenerPublicacionMasReciente();
        Task<List<Publicacion>> PublicacionFiltro(string filtro, int categoriaId);
        Task AddPublicacion(Publicacion publicacion);
        Task UpdatePublicacion(Publicacion publicacion);
        Task DeletePublicacion(int id);
    }
}
