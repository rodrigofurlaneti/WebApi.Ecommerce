using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;
using WebApi.Ecommerce.Data.Interface;
using WebApi.Ecommerce.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WebApi.Ecommerce.Data.Repository
{
    public class ContactUsRepository : IContactUsRepository
    {
        private readonly string _connectionString;

        public ContactUsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<ContactUs>> GetAsync()
        {
            List<ContactUs> list = new List<ContactUs>();
            string storedProcedureName = "Ecommerce_Procedure_ContactUs_GetAll";

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
                                list.Add(CreateContactUsFromReader(reader));
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

        public async Task PostAsync(ContactUs contactUs)
        {
            string storedProcedureName = "Ecommerce_Procedure_ContactUs_Insert";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adicionar parâmetros ao comando
                        command.Parameters.AddWithValue("@Name", contactUs.Name);
                        command.Parameters.AddWithValue("@Email", contactUs.Email);
                        command.Parameters.AddWithValue("@CellPhone", contactUs.CellPhone);
                        command.Parameters.AddWithValue("@Message", contactUs.Message);

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

        public async Task<ContactUs> GetByIdAsync(int id)
        {
            string storedProcedureName = "Ecommerce_Procedure_ContactUs_GetById";

            ContactUs contactUs = null;

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
                                contactUs = CreateContactUsFromReader(reader);
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

            return contactUs;
        }

        public async Task PutAsync(ContactUs contactUs)
        {
            string storedProcedureName = "Ecommerce_Procedure_ContactUs_Update";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        AddContactUsParameters(command, contactUs);

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
            string storedProcedureName = "Ecommerce_Procedure_ContactUs_Delete";

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

        private ContactUs CreateContactUsFromReader(SqlDataReader reader)
        {
            return new ContactUs
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                CellPhone = reader.GetString(reader.GetOrdinal("CellPhone")),
                Message = reader.GetString(reader.GetOrdinal("Message")),
                DateInsert = reader.GetDateTime(reader.GetOrdinal("DateInsert")),
                DateUpdate = reader.GetDateTime(reader.GetOrdinal("DateUpdate")),
                Status = reader.GetBoolean(reader.GetOrdinal("Status"))
            };
        }

        private void AddContactUsParameters(SqlCommand command, ContactUs contactUs)
        {
            command.Parameters.AddWithValue("@Id", contactUs.Id);
            command.Parameters.AddWithValue("@Name", contactUs.Name);
            command.Parameters.AddWithValue("@Email", contactUs.Email);
            command.Parameters.AddWithValue("@CellPhone", contactUs.CellPhone);
            command.Parameters.AddWithValue("@Message", contactUs.Message);
            command.Parameters.AddWithValue("@DateInsert", contactUs.DateInsert);
            command.Parameters.AddWithValue("@DateUpdate", contactUs.DateUpdate);
            command.Parameters.AddWithValue("@Status", contactUs.Status);
        }
    }
}
