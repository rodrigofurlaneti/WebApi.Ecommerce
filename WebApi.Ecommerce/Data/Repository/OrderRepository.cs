using Domain.Ecommerce.Enum;
using Domain.Ecommerce.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApi.Ecommerce.Data.Interface;

namespace WebApi.Ecommerce.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(IConfiguration configuration)
        {
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

        public async Task<int> PostAsync(Order order)
        {
            string storedProcedureName = "Ecommerce_Procedure_Order_Insert";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@IdUser", order.User.Id);
                        command.Parameters.AddWithValue("@IdProduct", order.Product.Id);
                        command.Parameters.AddWithValue("@Amount", order.Product.Amount);
                        command.Parameters.AddWithValue("@IdOrder", order.Id);

                        await connection.OpenAsync();

                        var result = await command.ExecuteScalarAsync();

                        if (result != null && int.TryParse(result.ToString(), out int orderId))
                        {
                            return orderId;
                        }
                        else
                        {
                            throw new Exception("Failed to retrieve the new order ID.");
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

        public async Task<Order?> GetByIdAsync(int id)
        {
            string storedProcedureName = "Ecommerce_Procedure_Order_X_Product_GetById";
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

        public async Task<int> GetProductCountByOrderIdAsync(int orderId)
        {
            string storedProcedureName = "Ecommerce_Procedure_Order_X_Product_CountById";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdOrder", orderId);

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
            command.Parameters.AddWithValue("@Id", order.Id);
            command.Parameters.AddWithValue("@IdUser", order.User.Id);
            command.Parameters.AddWithValue("@DateInsert", order.DateInsert);
            command.Parameters.AddWithValue("@DateUpdate", order.DateUpdate);
            command.Parameters.AddWithValue("@OrderStatus", (int)order.OrderStatus);
        }
    }
}
