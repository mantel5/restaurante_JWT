using Models;

namespace RestauranteAPI.Repositories
{
    public interface IComboRepository
    {
        Task<List<Combo>> GetAllAsync();
        Task<Combo?> GetByIdAsync(int id);
        Task AddAsync(Combo combo);
        Task UpdateAsync(Combo combo);
        Task DeleteAsync(int id);
    }
}
