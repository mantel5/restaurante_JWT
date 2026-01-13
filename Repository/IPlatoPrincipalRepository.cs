using Models;

namespace RestauranteAPI.Repositories
{
    public interface IPlatoPrincipalRepository
    {
        Task<List<PlatoPrincipal>> GetAllAsync();
        Task<PlatoPrincipal?> GetByIdAsync(int id);
        Task AddAsync(PlatoPrincipal plato);
        Task UpdateAsync(PlatoPrincipal plato);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}
