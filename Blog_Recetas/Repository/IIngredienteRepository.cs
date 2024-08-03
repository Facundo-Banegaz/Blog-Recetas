using Blog_Recetas.Models;

namespace Blog_Recetas.Repository
{
    public interface IIngredienteRepository
    {
        Task<IEnumerable<Ingrediente>> GetAll();
        Task<Ingrediente> GetId(int id);
        Task AddAIngrediente(Ingrediente ingrediente);
        Task UpdateIngrediente(Ingrediente ingrediente);
        Task DeleteIngrediente(int id);
    }
}
