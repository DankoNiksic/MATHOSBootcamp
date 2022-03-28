using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCRUD.Model;
using WebCRUD.Repository.Common;

namespace WebCRUD.Repository
{
    public class ProductRepository : IProductRepository
    {
        static string connectionString = @"Data Source=localhost\SQLEXPRESS01;Initial Catalog=WebAPI_CRUD;Integrated Security=True";

        public List<ProductModelEntity> listProducts = new List<ProductModelEntity>();
        public async Task<List<ProductModelEntity>> GetAllProductAsync()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand(
                    "SELECT * FROM Product;", connection
                    );
                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductModelEntity model = new ProductModelEntity
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Price = (int)reader["Price"]
                        };
                        listProducts.Add(model);
                    }
                }
                reader.Close();
                connection.Close();
            }
            return listProducts;
        }
        public async Task<ProductModelEntity> GetProductByIdAsync(int id)
        {

            var connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM Product WHERE Product.Id={id};", connection
                    );
                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();
                ProductModelEntity model = new ProductModelEntity();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        model.Id = (int)reader["Id"];
                        model.Name = reader["Name"].ToString();
                        model.Price = (int)reader["Price"];
                    }
                }
                reader.Close();
                connection.Close();
                return model;
            }
        }
        public async Task<ProductModelEntity> GetProductByNameAsync(string name)
        {

            var connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM Product WHERE Product.Name='{name}';", connection
                    );
                connection.Open();

                SqlDataReader reader =await command.ExecuteReaderAsync();
                ProductModelEntity model = new ProductModelEntity();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        model.Id = (int)reader["Id"];
                        model.Name = reader["Name"].ToString();
                        model.Price = (int)reader["Price"];
                    }
                }
                reader.Close();
                connection.Close();
                return model;
            }
        }
        public async Task CreateNewProductAsync(ProductModelEntity product)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            var connection = new SqlConnection(connectionString);
            using (connection)
            {
                string sql = $"insert into Product (Name,Price) values('{product.Name}',{product.Price})";

                connection.Open();

                adapter.InsertCommand = new SqlCommand(sql, connection);
               await adapter.InsertCommand.ExecuteNonQueryAsync();

                connection.Close();
            }
        }
        public async Task CreateNewProductsAsync(List<ProductModelEntity> product)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            var connection = new SqlConnection(connectionString);
            using (connection)
            {
                string sql= "insert into Product (Name,Price) values";
                foreach (var item in product)
                {
                  sql += $" ('{item.Name}',{item.Price}),";
                }
                sql = sql.Remove(sql.Length - 1, 1) + ";";
                connection.Open();

                adapter.InsertCommand = new SqlCommand(sql, connection);
               await adapter.InsertCommand.ExecuteNonQueryAsync();

                connection.Close();
            }
        }
        public async Task EditProductAsync(ProductModelEntity product)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            var connection = new SqlConnection(connectionString);
            using (connection)
            {
                string sql = $"update Product set Name = '{product.Name}', Price={product.Price} where Id ={product.Id}";

                connection.Open();

                adapter.UpdateCommand = connection.CreateCommand();
                adapter.UpdateCommand.CommandText = sql;
               await adapter.UpdateCommand.ExecuteNonQueryAsync();

                connection.Close();
            }
        }
        public async Task DeleteProductAsync(int id)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            var connection = new SqlConnection(connectionString);
            using (connection)
            {
                string sql = $"delete Product where Id ={id}";

                connection.Open();

                adapter.DeleteCommand = connection.CreateCommand();
                adapter.DeleteCommand.CommandText = sql;
               await adapter.DeleteCommand.ExecuteNonQueryAsync();

                connection.Close();
            }
        }
        public async Task<bool> CheckIdAsync(int id)
        {
            var connection = new SqlConnection(connectionString);
            bool checkId;
            using (connection)
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM Product WHERE Product.Id={id};", connection
                    );
                connection.Open();

                SqlDataReader reader =await command.ExecuteReaderAsync();

                if (!reader.HasRows)
                {
                    checkId = false;
                }
                else
                {
                    checkId = true;
                }
                reader.Close();
                connection.Close();
                return checkId;
            }
        }
    }
}
