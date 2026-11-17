using Microsoft.Data.SqlClient;
using Lab09.Models;
using System.Data;

namespace Lab09.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Database connection string 'DefaultConnection' is missing.");
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var products = new List<Product>();

            const string sql = """
                SELECT Id, Title, [description], [image],
                       price, quantity, status
                FROM dbo.products
                ORDER BY Id DESC;
                """;

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                products.Add(MapProduct(reader));
            }

            return products;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            const string sql = """
                SELECT Id, Title, [description], [image],
                       price, quantity, status
                FROM dbo.products
                WHERE Id = @Id;
                """;

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);
            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return MapProduct(reader);
            }

            return null;
        }

        public async Task CreateAsync(Product product)
        {
            const string sql = """
                INSERT INTO dbo.products
                    (Title, [description], [image],
                     price, quantity, status)
                VALUES
                    (@Title, @Description, @Image,
                     @Price, @Quantity, @Status);
                """;

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            AddProductParameters(command, product);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            const string sql = """
                UPDATE dbo.products
                SET Title = @Title,
                    [description] = @Description,
                    [image] = @Image,
                    price = @Price,
                    quantity = @Quantity,
                    status = @Status
                WHERE Id = @Id;
                """;

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            AddProductParameters(command, product);
            command.Parameters.Add("@Id", SqlDbType.Int).Value = product.Id;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            const string sql = "DELETE FROM dbo.products WHERE Id = @Id;";

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        private static void AddProductParameters(SqlCommand command, Product product)
        {
            command.Parameters.Add("@Title", SqlDbType.VarChar, 50).Value =
                (object?)product.Title ?? DBNull.Value;

            command.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value =
                (object?)product.Description ?? DBNull.Value;

            command.Parameters.Add("@Image", SqlDbType.VarChar, 255).Value =
                (object?)product.Image ?? DBNull.Value;

            command.Parameters.Add("@Price", SqlDbType.Int).Value = product.Price;

            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = product.Quantity;

            command.Parameters.Add("@Status", SqlDbType.VarChar, 150).Value =
                (object?)product.Status ?? DBNull.Value;
        }

        private static Product MapProduct(SqlDataReader reader)
        {
            int titleOrdinal = reader.GetOrdinal("Title");
            int descOrdinal = reader.GetOrdinal("description");
            int imgOrdinal = reader.GetOrdinal("image");
            int priceOrdinal = reader.GetOrdinal("price");
            int qtyOrdinal = reader.GetOrdinal("quantity");
            int statusOrdinal = reader.GetOrdinal("status");

            return new Product
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Title = reader.IsDBNull(titleOrdinal) ? string.Empty : reader.GetString(titleOrdinal),
                Description = reader.IsDBNull(descOrdinal) ? string.Empty : reader.GetString(descOrdinal),
                Image = reader.IsDBNull(imgOrdinal) ? string.Empty : reader.GetString(imgOrdinal),
                Price = reader.IsDBNull(priceOrdinal) ? 0 : reader.GetInt32(priceOrdinal),
                Quantity = reader.IsDBNull(qtyOrdinal) ? 0 : reader.GetInt32(qtyOrdinal),
                Status = reader.IsDBNull(statusOrdinal) ? string.Empty : reader.GetString(statusOrdinal)
            };
        }
    }
}
