using Domain.Ecommerce.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApi.Ecommerce.Data.Interface;

namespace WebApi.Ecommerce.Data.Repository
{
    public class AccessLogRepository : IAccessLogRepository
    {
        private readonly string _connectionString;

        public AccessLogRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(configuration), "Connection string cannot be null");
        }

        public async Task<IEnumerable<AccessLog>> GetAsync()
        {
            List<AccessLog> list = new List<AccessLog>();
            string storedProcedureName = "Ecommerce_Procedure_AccessLog_GetAll";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(CreateFromReader(reader));
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

            return list;
        }

        public async Task PostAsync(AccessLog accessLog)
        {
            string storedProcedureName = "Ecommerce_Procedure_AccessLog_Insert";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Latitude", accessLog.Latitude);
                        command.Parameters.AddWithValue("@Longitude", accessLog.Longitude);
                        command.Parameters.AddWithValue("@UserAgent", accessLog.UserAgent);
                        command.Parameters.AddWithValue("@InternetProtocol", accessLog.InternetProtocol);

                        await connection.OpenAsync();

                        await command.ExecuteScalarAsync();
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
        }

        public async Task<AccessLog?> GetByIdAsync(int id)
        {
            string storedProcedureName = "Ecommerce_Procedure_AccessLog_X_Product_GetById";
            AccessLog? order = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                order = CreateFromReader(reader);
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

            return order;
        }

        public async Task<int> GetProductCountByAccessLogIdAsync(int orderId)
        {
            string storedProcedureName = "Ecommerce_Procedure_AccessLog_X_Product_CountById";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdAccessLog", orderId);

                        await connection.OpenAsync();

                        var result = await command.ExecuteScalarAsync();

                        if (result != null && int.TryParse(result.ToString(), out int productCount))
                        {
                            return productCount;
                        }
                        else
                        {
                            throw new Exception("Failed to retrieve the product count.");
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
        }

        public async Task PutAsync(AccessLog order)
        {
            string storedProcedureName = "Ecommerce_Procedure_AccessLog_Update";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        AddParameters(command, order);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
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
        }

        public async Task DeleteAsync(int id)
        {
            string storedProcedureName = "Ecommerce_Procedure_AccessLog_Delete";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
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
        }

        private AccessLog CreateFromReader(SqlDataReader reader)
        {
            return new AccessLog
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Latitude = reader.GetString(reader.GetOrdinal("Latitude")),
                Longitude = reader.GetString(reader.GetOrdinal("Longitude")),
                InternetProtocol = reader.GetString(reader.GetOrdinal("InternetProtocol")),
                UserAgent = reader.GetString(reader.GetOrdinal("UserAgent")),
                DateInsert = reader.GetDateTime(reader.GetOrdinal("DateInsert"))
            };
        }

        private void AddParameters(SqlCommand command, AccessLog accessLog)
        {
            command.Parameters.AddWithValue("@Id", accessLog.Id);
            command.Parameters.AddWithValue("@Latitude", accessLog.Latitude);
            command.Parameters.AddWithValue("@Longitude", accessLog.Longitude);
            command.Parameters.AddWithValue("@InternetProtocol", accessLog.InternetProtocol);
            command.Parameters.AddWithValue("@UserAgent", accessLog.UserAgent);
            command.Parameters.AddWithValue("@DateInsert", accessLog.DateInsert);
        }

    }
}
