using Models;

namespace RestauranteAPI.Repositories
{
    public interface IBebidaRepository
    {
        Task<List<Bebida>> GetAllAsync();
        Task<Bebida?> GetByIdAsync(int id);
        Task AddAsync(Bebida bebida);
        Task UpdateAsync(Bebida bebida);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}
