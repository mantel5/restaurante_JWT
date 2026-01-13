using RestauranteAPI.Repositories;

namespace RestauranteAPI.Services
{
    public class PlatoPrincipalService : IPlatoPrincipalService
    {
        private readonly IPlatoPrincipalRepository _platoPrincipalRepository;

        public PlatoPrincipalService(IPlatoPrincipalRepository platoPrincipalRepository)
        {
            _platoPrincipalRepository = platoPrincipalRepository;
            
        }

        public async Task<List<PlatoPrincipal>> GetAllAsync()
        {
            return await _platoPrincipalRepository.GetAllAsync();
        }

        public async Task<PlatoPrincipal?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _platoPrincipalRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(PlatoPrincipal plato)
        {
            if (string.IsNullOrWhiteSpace(plato.Nombre))
                throw new ArgumentException("El nombre del plato no puede estar vacío.");

            if (plato.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor que cero.");

            await _platoPrincipalRepository.AddAsync(plato);
        }

        public async Task UpdateAsync(PlatoPrincipal plato)
        {
            if (plato.Id <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

            if (string.IsNullOrWhiteSpace(plato.Nombre))
                throw new ArgumentException("El nombre del plato no puede estar vacío.");

            await _platoPrincipalRepository.UpdateAsync(plato);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _platoPrincipalRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {
            await _platoPrincipalRepository.InicializarDatosAsync();
        }
    }
}
