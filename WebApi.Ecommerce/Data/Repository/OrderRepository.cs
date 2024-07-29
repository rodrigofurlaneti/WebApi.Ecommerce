using Domain.Ecommerce.Enum;
using Domain.Ecommerce.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApi.Ecommerce.Data.Interface;
using Microsoft.Extensions.Configuration;

namespace WebApi.Ecommerce.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(IConfiguration configuration)
        {
            // Garante que _connectionString seja inicializado corretamente
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(configuration), "Connection string cannot be null");
        }

        public async Task<IEnumerable<Order>> GetAsync()
        {
            List<Order> list = new List<Order>();

            string storedProcedureName = "Ecommerce_Procedure_Order_GetAll";

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

        public async Task PostAsync(Order order)
        {
            string storedProcedureName = "Ecommerce_Procedure_Order_Insert";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adicionar parâmetros ao comando
                        command.Parameters.AddWithValue("@IdUser", order.User.Id);
                        command.Parameters.AddWithValue("@OrderStatus", order.OrderStatus);

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

        public async Task<Order?> GetByIdAsync(int id)
        {
            string storedProcedureName = "Ecommerce_Procedure_Order_GetById";

            Order? order = null;

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

        public async Task PutAsync(Order order)
        {
            string storedProcedureName = "Ecommerce_Procedure_Order_Update";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adicionar apenas os parâmetros necessários
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
            string storedProcedureName = "Ecommerce_Procedure_Order_Delete";

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

        private Order CreateFromReader(SqlDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
                Console.WriteLine(reader.GetName(i) + " - " + reader.GetFieldType(i));

            return new Order
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                User = new User() { Id = reader.GetInt32(reader.GetOrdinal("IdUser")) },
                DateInsert = reader.GetDateTime(reader.GetOrdinal("DateInsert")),
                DateUpdate = reader.GetDateTime(reader.GetOrdinal("DateUpdate")),
                OrderStatus = (OrderStatus)reader.GetInt32(reader.GetOrdinal("OrderStatus"))
            };
        }

        private void AddParameters(SqlCommand command, Order order)
        {
            var parameters = new (string, object)[]
            {
                    ("@Id", order.Id),
                    ("@IdUser", order.User.Id),
                    ("@DateInsert", order.DateInsert),
                    ("@DateUpdate", order.DateUpdate),
                    ("@OrderStatus", (int)order.OrderStatus)
            };

            foreach (var (name, value) in parameters)
            {
                Console.WriteLine($"{name}: {value}");
                command.Parameters.AddWithValue(name, value);
            }
        }
    }
}
