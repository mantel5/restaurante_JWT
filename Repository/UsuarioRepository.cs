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
                ?? throw new Exception("Error de conexión");
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    SELECT u.Id, u.Nombre, u.Email, u.Password, u.RolId, r.Nombre as NombreRol
                    FROM Usuario u
                    INNER JOIN Rol r ON u.RolId = r.Id
                    WHERE u.Email = @Email";

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
                string query = @"
                    SELECT u.Id, u.Nombre, u.Email, u.Password, u.RolId, r.Nombre as NombreRol
                    FROM Usuario u
                    INNER JOIN Rol r ON u.RolId = r.Id
                    WHERE u.Id = @Id";

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
                RolId = Convert.ToInt32(reader["RolId"]),
                
                Rol = new Rol 
                { 
                    Id = Convert.ToInt32(reader["RolId"]),
                    Nombre = reader["NombreRol"].ToString() 
                }
            };
        }
    }
}