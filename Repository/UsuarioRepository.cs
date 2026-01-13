using System.Data;
using Microsoft.Data.SqlClient;
using RestauranteAPI.Models;
using RestauranteAPI.Repositories;

namespace RestauranteAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RestauranteDB") 
                ?? throw new Exception("¡Ojo! No encuentro 'RestauranteDB' en appsettings.json");
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Nombre, Email, Password, Rol FROM Usuario WHERE Email = @Email";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) return MapReaderToUsuario(reader);
                    }
                }
            }
            return null;
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Nombre, Email, Password, Rol FROM Usuario WHERE Id = @Id";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) return MapReaderToUsuario(reader);
                    }
                }
            }
            return null;
        }

        private Usuario MapReaderToUsuario(SqlDataReader reader)
        {
            return new Usuario
            {
                Id = Convert.ToInt32(reader["Id"]),
                Nombre = reader["Nombre"].ToString(),
                Email = reader["Email"].ToString(),
                Password = reader["Password"].ToString(),
                Rol = reader["Rol"].ToString()
            };
        }
    }
}