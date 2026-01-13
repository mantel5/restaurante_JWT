
using Microsoft.Data.SqlClient;

namespace RestauranteAPI.Repositories
{
    public class PlatoPrincipalRepository : IPlatoPrincipalRepository
    {
        private readonly string _connectionString;

        public PlatoPrincipalRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RestauranteDB") ?? "Not found";
        }

        public async Task<List<PlatoPrincipal>> GetAllAsync()
        {
            var platosPrincipales = new List<PlatoPrincipal>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Precio, Ingredientes FROM PlatoPrincipal";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var plato = new PlatoPrincipal
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Precio = (double)reader.GetDecimal(2),
                                Ingredientes = reader.GetString(3)
                            }; 

                            platosPrincipales.Add(plato);
                        }
                    }
                }
            }
            return platosPrincipales;
        }

        public async Task<PlatoPrincipal> GetByIdAsync(int id)
        {
            PlatoPrincipal platoPrincipal = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Precio, Ingredientes FROM PlatoPrincipal WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            platoPrincipal = new PlatoPrincipal
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Precio = Convert.ToDouble(reader.GetDecimal(2)),
                                Ingredientes = reader.GetString(3)
                            };
                        }
                    }
                }
            }
            return platoPrincipal;
        }

        public async Task AddAsync(PlatoPrincipal platoPrincipal)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO PlatoPrincipal (Nombre, Precio, Ingredientes) VALUES (@Nombre, @Precio, @Ingredientes)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", platoPrincipal.Nombre);
                    command.Parameters.AddWithValue("@Precio", platoPrincipal.Precio);
                    command.Parameters.AddWithValue("@Ingredientes", platoPrincipal.Ingredientes);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(PlatoPrincipal platoPrincipal)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE PlatoPrincipal SET Nombre = @Nombre, Precio = @Precio, Ingredientes = @Ingredientes WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", platoPrincipal.Id);
                    command.Parameters.AddWithValue("@Nombre", platoPrincipal.Nombre);
                    command.Parameters.AddWithValue("@Precio", platoPrincipal.Precio);
                    command.Parameters.AddWithValue("@Ingredientes", platoPrincipal.Ingredientes);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM PlatoPrincipal WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task InicializarDatosAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Comando SQL para insertar datos iniciales
                var query = @"
                    INSERT INTO PlatoPrincipal (Nombre, Precio, Ingredientes)
                    VALUES 
                    (@Nombre1, @Precio1, @Ingredientes1),
                    (@Nombre2, @Precio2, @Ingredientes2)";

                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para el primer plato
                    command.Parameters.AddWithValue("@Nombre1", "Plato combinado");
                    command.Parameters.AddWithValue("@Precio1", 12.50);
                    command.Parameters.AddWithValue("@Ingredientes1", "Pollo, patatas, tomate");

                    // Parámetros para el segundo plato
                    command.Parameters.AddWithValue("@Nombre2", "Plato vegetariano");
                    command.Parameters.AddWithValue("@Precio2", 10.00);
                    command.Parameters.AddWithValue("@Ingredientes2", "Tofu, verduras, arroz");

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}