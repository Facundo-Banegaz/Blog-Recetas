using Blog_Recetas.Models;

namespace Blog_Recetas.Services
{
    public interface IIngredienteService
    {
        Task<IEnumerable<Ingrediente>> GetAll();
        Task<Ingrediente> GetId(int id);
        Task AddAIngrediente(Ingrediente ingrediente);
        Task UpdateIngrediente(Ingrediente ingrediente);
        Task DeleteIngrediente(int id);
    }
}
