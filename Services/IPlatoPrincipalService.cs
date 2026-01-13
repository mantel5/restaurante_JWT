namespace RestauranteAPI.Services
{
    public interface IPlatoPrincipalService
    {
        Task<List<PlatoPrincipal>> GetAllAsync();
        Task<PlatoPrincipal?> GetByIdAsync(int id);
        Task AddAsync(PlatoPrincipal plato);
        Task UpdateAsync(PlatoPrincipal plato);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();

    }
}
