using Domain.Ecommerce.Enum;
using Domain.Ecommerce.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApi.Ecommerce.Data.Interface;

namespace WebApi.Ecommerce.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            // Garante que _connectionString seja inicializado corretamente
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(configuration), "Connection string cannot be null");
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            List<Product> list = new List<Product>();

            string storedProcedureName = "Ecommerce_Procedure_Product_GetAll";

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

        public async Task PostAsync(Product product)
        {
            string storedProcedureName = "Ecommerce_Procedure_Product_Insert";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adicionar parâmetros ao comando
                        command.Parameters.AddWithValue("@Name", product.Name);
                        command.Parameters.AddWithValue("@Amount", product.Amount);
                        command.Parameters.AddWithValue("@Details", product.Details);
                        command.Parameters.AddWithValue("@Picture", product.Picture);
                        command.Parameters.AddWithValue("@ValueOf", product.ValueOf);
                        command.Parameters.AddWithValue("@ValueFor", product.ValueFor);
                        command.Parameters.AddWithValue("@Discount", product.Discount);
                        command.Parameters.AddWithValue("@ProductStatus", (int)product.ProductStatus);

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

        public async Task<Product?> GetByIdAsync(int id)
        {
            string storedProcedureName = "Ecommerce_Procedure_Product_GetById";

            Product? product = null;

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
                                product = CreateFromReader(reader);
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

            return product;
        }

        public async Task PutAsync(Product product)
        {
            string storedProcedureName = "Ecommerce_Procedure_Product_Update";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adicionar apenas os parâmetros necessários
                        AddParameters(command, product);

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
            string storedProcedureName = "Ecommerce_Procedure_Product_Delete";

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

        private Product CreateFromReader(SqlDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
                Console.WriteLine(reader.GetName(i) + " - " + reader.GetFieldType(i));

            return new Product
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Amount = reader.GetInt32(reader.GetOrdinal("Amount")),
                Details = reader.GetString(reader.GetOrdinal("Details")),
                Picture = reader.GetString(reader.GetOrdinal("Picture")),
                ValueOf = reader.GetDecimal(reader.GetOrdinal("ValueOf")),
                ValueFor = reader.GetDecimal(reader.GetOrdinal("ValueFor")),
                Discount = reader.GetDecimal(reader.GetOrdinal("Discount")),
                DateInsert = reader.GetDateTime(reader.GetOrdinal("DateInsert")),
                DateUpdate = reader.GetDateTime(reader.GetOrdinal("DateUpdate")),
                ProductStatus = (ProductStatus)reader.GetInt32(reader.GetOrdinal("ProductStatus"))
            };
        }

        private void AddParameters(SqlCommand command, Product product)
        {
            var parameters = new (string, object?)[]
            {
                    ("@Id", product.Id),
                    ("@Name", product.Name),
                    ("@Amount", product.Amount),
                    ("@Details", product.Details),
                    ("@Picture", product.Picture),
                    ("@ValueOf", product.ValueOf),
                    ("@ValueFor", product.ValueFor),
                    ("@ValueOf", product.ValueOf),
                    ("@Discount", product.Discount),
                    ("@DateInsert", product.DateInsert),
                    ("@DateUpdate", product.DateUpdate),
                    ("@OrderStatus", (OrderStatus)product.ProductStatus)
            };

            foreach (var (name, value) in parameters)
            {
                Console.WriteLine($"{name}: {value}");
                command.Parameters.AddWithValue(name, value);
            }

        }
    }
}
