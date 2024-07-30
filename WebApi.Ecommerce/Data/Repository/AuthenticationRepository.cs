using Domain.Ecommerce.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApi.Ecommerce.Data.Interface;

namespace WebApi.Ecommerce.Data.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly string _connectionString;

        public AuthenticationRepository(IConfiguration configuration)
        {
            // Garante que _connectionString seja inicializado corretamente
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(configuration), "Connection string cannot be null");
        }
        public async Task<AuthenticationResponse?> PostAsync(AuthenticationRequest authenticationRequest)
        {
            string storedProcedureName = "Ecommerce_Procedure_Authentication_Post";

            AuthenticationResponse? authentication = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", authenticationRequest.Username);
                        command.Parameters.AddWithValue("@Password", authenticationRequest.Password);

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                authentication = CreateFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.Error.WriteLine($"Erro de SQL: {sqlEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro: {ex.Message}");
                throw;
            }

            return authentication;
        }

        private AuthenticationResponse CreateFromReader(SqlDataReader reader)
        {
            return new AuthenticationResponse
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                Password = reader.GetString(reader.GetOrdinal("Password")),
                Status = reader.GetBoolean(reader.GetOrdinal("Status")),
            };
        }
    }
}
