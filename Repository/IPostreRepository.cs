using Models;

namespace RestauranteAPI.Repositories
{
    public interface IPostreRepository
    {
        Task<List<Postre>> GetAllAsync();
        Task<Postre?> GetByIdAsync(int id);
        Task AddAsync(Postre postre);
        Task UpdateAsync(Postre postre);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}
