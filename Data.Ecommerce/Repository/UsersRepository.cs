using System.Data;
using Data.Ecommerce.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Domain.Ecommerce.Model;
using Domain.Ecommerce.Enum;

namespace Data.Ecommerce.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly string _connectionString;

        public UsersRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            List<User> list = new List<User>();
            string storedProcedureName = "Ecommerce_Procedure_User_GetAll";

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
                                list.Add(CreateUserFromReader(reader));
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

        public async Task PostAsync(User user)
        {
            string storedProcedureName = "Ecommerce_Procedure_User_Insert";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adicionar parâmetros ao comando
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@Address", user.Address);
                        command.Parameters.AddWithValue("@Number", user.Number);
                        command.Parameters.AddWithValue("@Complement", user.Complement);
                        command.Parameters.AddWithValue("@Neighborhood", user.Neighborhood);
                        command.Parameters.AddWithValue("@City", user.City);
                        command.Parameters.AddWithValue("@State", user.State);
                        command.Parameters.AddWithValue("@ZipCode", user.ZipCode);
                        command.Parameters.AddWithValue("@CellPhone", user.CellPhone);
                        command.Parameters.AddWithValue("@Username", user.Username);
                        command.Parameters.AddWithValue("@Password", user.Password);
                        command.Parameters.AddWithValue("ProfileId", "2");

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

        public async Task<User> GetByIdAsync(int id)
        {
            string storedProcedureName = "Ecommerce_Procedure_User_GetById";

            User user = null;

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
                                user = CreateUserFromReader(reader);
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

            return user;
        }

        public async Task PutAsync(User user)
        {
            string storedProcedureName = "Ecommerce_Procedure_User_Update";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        AddUserParameters(command, user);

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
            string storedProcedureName = "Ecommerce_Procedure_User_Delete";

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

        private User CreateUserFromReader(SqlDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.WriteLine(reader.GetName(i) + " - " + reader.GetFieldType(i));
            }

            return new User
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                Address = reader.GetString(reader.GetOrdinal("Address")),
                Number = reader.GetString(reader.GetOrdinal("Number")),
                Complement = reader.GetString(reader.GetOrdinal("Complement")),
                Neighborhood = reader.GetString(reader.GetOrdinal("Neighborhood")),
                City = reader.GetString(reader.GetOrdinal("City")),
                State = reader.GetString(reader.GetOrdinal("State")),
                ZipCode = reader.GetString(reader.GetOrdinal("ZipCode")),
                CellPhone = reader.GetString(reader.GetOrdinal("CellPhone")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                Password = reader.GetString(reader.GetOrdinal("Password")),
                DateInsert = reader.GetDateTime(reader.GetOrdinal("DateInsert")),
                DateUpdate = reader.GetDateTime(reader.GetOrdinal("DateUpdate")),
                Status = reader.GetBoolean(reader.GetOrdinal("Status")) ? Status.Active : Status.Disabled
            };
        }

        private void AddUserParameters(SqlCommand command, User user)
        {
            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Address", user.Address);
            command.Parameters.AddWithValue("@Number", user.Number);
            command.Parameters.AddWithValue("@Complement", user.Complement);
            command.Parameters.AddWithValue("@Neighborhood", user.Neighborhood);
            command.Parameters.AddWithValue("@City", user.City);
            command.Parameters.AddWithValue("@State", user.State);
            command.Parameters.AddWithValue("@ZipCode", user.ZipCode);
            command.Parameters.AddWithValue("@CellPhone", user.CellPhone);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@DateInsert", user.DateInsert);
            command.Parameters.AddWithValue("@DateUpdate", user.DateUpdate);
            command.Parameters.AddWithValue("@Status", (int)user.Status);
        }
    }
}
