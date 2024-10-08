﻿using Domain.Ecommerce.Enum;
using Domain.Ecommerce.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using Data.Ecommerce.Interface;
using Microsoft.Extensions.Configuration;

namespace Data.Ecommerce.Repository
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly string _connectionString;

        public OrderProductRepository(IConfiguration configuration)
        {
            // Garante que _connectionString seja inicializado corretamente
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(configuration), "Connection string cannot be null");
        }

        public async Task<IEnumerable<OrderProduct>> GetAsync()
        {
            List<OrderProduct> list = new List<OrderProduct>();

            string storedProcedureName = "Ecommerce_Procedure_Order_X_Product_GetAll";

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

        public async Task<List<OrderProductResponse>> PostAsync(OrderProductRequest orderProductRequest)
        {
            string storedProcedureName = "Ecommerce_Procedure_Order_X_Product_GetByOrderId";

            List<OrderProductResponse> listOrderProductResponse = new List<OrderProductResponse>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adicionar parâmetros ao comando
                        command.Parameters.AddWithValue("@IdOrder", orderProductRequest.IdOrder);

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                OrderProductResponse response = new OrderProductResponse
                                {
                                    Product = new List<Product>
                                    {
                                        new Product
                                        {
                                            Id = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                            Name = reader.GetString(reader.GetOrdinal("ProductName")),
                                            Details = reader.GetString(reader.GetOrdinal("ProductDetails")),
                                            Amount = reader.GetInt32(reader.GetOrdinal("OrderProductAmount")),
                                            Picture = reader.GetString(reader.GetOrdinal("ProductPicture")),
                                            ValueOf = reader.GetDecimal(reader.GetOrdinal("ProductValueOf")),
                                            ValueFor = reader.GetDecimal(reader.GetOrdinal("ProductValueFor")),
                                            Discount = reader.GetDecimal(reader.GetOrdinal("ProductDiscount")),
                                            DateInsert = reader.GetDateTime(reader.GetOrdinal("ProductDateInsert")),
                                            DateUpdate = reader.GetDateTime(reader.GetOrdinal("ProductDateUpdate")),
                                            // Outros campos da tabela de produtos
                                        }
                                    }
                                };
                                listOrderProductResponse.Add(response);
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

            return listOrderProductResponse;
        }

        public async Task<OrderProduct?> GetByIdAsync(int id)
        {
            string storedProcedureName = "Ecommerce_Procedure_Order_X_Product_GetById";

            OrderProduct? orderProduct = null;

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
                                orderProduct = CreateFromReader(reader);
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

            return orderProduct;
        }

        public async Task PutAsync(OrderProduct orderProduct)
        {
            string storedProcedureName = "Ecommerce_Procedure_Order_X_Product_Update";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adicionar apenas os parâmetros necessários
                        AddParameters(command, orderProduct);

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
            string storedProcedureName = "Ecommerce_Procedure_Order_X_Product_Delete";

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

        private OrderProduct CreateFromReader(SqlDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
                Console.WriteLine(reader.GetName(i) + " - " + reader.GetFieldType(i));

            return new OrderProduct
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                IdOrder = reader.GetInt32(reader.GetOrdinal("IdOrder")),
                IdProduct = reader.GetInt32(reader.GetOrdinal("IdProduct")),
                DateInsert = reader.GetDateTime(reader.GetOrdinal("DateInsert")),
                DateUpdate = reader.GetDateTime(reader.GetOrdinal("DateUpdate")),
                Status = reader.GetBoolean(reader.GetOrdinal("Status")) ? Status.Active : Status.Disabled
            };
        }

        private void AddParameters(SqlCommand command, OrderProduct orderProduct)
        {
            AddParameter(command, "@Id", orderProduct.Id);
            AddParameter(command, "@IdOrder", orderProduct.IdOrder);
            AddParameter(command, "@IdProduct", orderProduct.IdProduct);
            AddParameter(command, "@DateInsert", orderProduct.DateInsert);
            AddParameter(command, "@DateUpdate", orderProduct.DateUpdate);
            AddParameter(command, "@Status", (int)orderProduct.Status);
        }

        private void AddParameter(SqlCommand command, string parameterName, object value)
        {
            Console.WriteLine($"{parameterName}: {value}");
            command.Parameters.AddWithValue(parameterName, value);
        }
    }
}
