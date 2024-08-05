using Domain.Ecommerce.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApi.Ecommerce.Data.Interface;

namespace WebApi.Ecommerce.Data.Repository
{
    public class GeolocationRepository : IGeolocationRepository
    {
        private readonly string _connectionString;

        public GeolocationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                    ?? throw new ArgumentNullException(nameof(configuration), "Connection string cannot be null");
        }

        public async Task PostAsync(Place place)
        {
            string storedProcedureName = "Ecommerce_Procedure_Geolocation_Place_Insert";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PlaceId", place.PlaceId);
                        command.Parameters.AddWithValue("@Licence", place.Licence ?? (object)DBNull.Value); // Tratamento para valores nulos
                        command.Parameters.AddWithValue("@OsmType", place.OsmType ?? (object)DBNull.Value); // Tratamento para valores nulos
                        command.Parameters.AddWithValue("@OsmId", place.OsmId);
                        command.Parameters.AddWithValue("@Latitude", place.Lat ?? (object)DBNull.Value); // Tratamento para valores nulos
                        command.Parameters.AddWithValue("@Longitude", place.Lon ?? (object)DBNull.Value); // Tratamento para valores nulos
                        command.Parameters.AddWithValue("@DisplayName", place.DisplayName ?? (object)DBNull.Value); // Tratamento para valores nulos
                        command.Parameters.AddWithValue("@HouseNumber", place.Address?.HouseNumber ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Road", place.Address?.Road ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Suburb", place.Address?.Suburb ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@City", place.Address?.City ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Municipality", place.Address?.Municipality ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@County", place.Address?.County ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@StateDistrict", place.Address?.StateDistrict ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@State", place.Address?.State ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ISO3166_2_lvl4", place.Address?.ISO3166_2_lvl4 ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Region", place.Address?.Region ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Postcode", place.Address?.Postcode ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Country", place.Address?.Country ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CountryCode", place.Address?.CountryCode ?? (object)DBNull.Value);
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
    }
}
