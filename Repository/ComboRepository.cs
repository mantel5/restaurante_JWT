
using Microsoft.Data.SqlClient;
using Models;

namespace RestauranteAPI.Repositories
{
    public class ComboRepository : IComboRepository
    {
        private readonly string _connectionString;

        private readonly IPlatoPrincipalRepository _platoprincipalrepository;
        private readonly IBebidaRepository _bebidarepository;
        private readonly IPostreRepository _postrerepository;

        public ComboRepository(IConfiguration configuration, IPlatoPrincipalRepository platoprincipalrepository, IBebidaRepository bebidarepository, IPostreRepository postrerepository)
        {
             _connectionString = configuration.GetConnectionString("RestauranteDB") ?? "Not found";
            _platoprincipalrepository = platoprincipalrepository;
            _bebidarepository = bebidarepository;
            _postrerepository = postrerepository;
        }
        

        public async Task<List<Combo>> GetAllAsync()
        {
            var combos = new List<Combo>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, PlatoPrincipal, Bebida, Postre, Descuento FROM Combo";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var combo = new Combo
                            {
                                Id = reader.GetInt32(0),
                                PlatoPrincipal = await _platoprincipalrepository.GetByIdAsync(reader.GetInt32(1)),
                                Bebida = await _bebidarepository.GetByIdAsync(reader.GetInt32(2)),
                                Postre = await _postrerepository.GetByIdAsync(reader.GetInt32(3)),
                                Descuento = Convert.ToDouble(reader.GetDecimal(4)),
                            }; 

                            combos.Add(combo);
                        }
                    }
                }
            }
            return combos;
        }

        public async Task<Combo> GetByIdAsync(int id)
        {
            Combo combo = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, PlatoPrincipal, Bebida, Postre, Descuento FROM Combo WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            combo = new Combo
                            {
                                Id = reader.GetInt32(0),
                                PlatoPrincipal = await _platoprincipalrepository.GetByIdAsync(reader.GetInt32(1)),
                                Bebida = await _bebidarepository.GetByIdAsync(reader.GetInt32(2)),
                                Postre = await _postrerepository.GetByIdAsync(reader.GetInt32(3)),
                                Descuento = Convert.ToDouble(reader.GetDecimal(4)),
                            };
                        }
                    }
                }
            }
            return combo;
        }

        public async Task AddAsync(Combo combo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Combo (PlatoPrincipal, Bebida, Postre, Descuento) VALUES (@PlatoPrincipal, @Bebida, @Postre, @Descuento)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PlatoPrincipal", combo.PlatoPrincipal.Id);
                    command.Parameters.AddWithValue("@Bebida", combo.Bebida.Id);
                    command.Parameters.AddWithValue("@Postre", combo.Postre.Id);
                    command.Parameters.AddWithValue("@Descuento", combo.Descuento);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Combo combo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Combo SET PlatoPrincipal = @PlatoPrincipal, Bebida = @Bebida, Postre = @Postre, Descuento = @Descuento WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", combo.Id);
                    command.Parameters.AddWithValue("@PlatoPrincipal", combo.PlatoPrincipal.Id);
                    command.Parameters.AddWithValue("@Bebida", combo.Bebida.Id);
                    command.Parameters.AddWithValue("@Postre", combo.Postre.Id);
                    command.Parameters.AddWithValue("@Descuento", combo.Descuento);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Combo WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}