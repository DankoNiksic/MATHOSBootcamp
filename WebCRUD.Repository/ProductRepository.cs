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
        public List<ProductModelEntity> GetAllProduct()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand(
                    "SELECT * FROM Product;", connection
                    );
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

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
        public ProductModelEntity GetProductById(int id)
        {

            var connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM Product WHERE Product.Id={id};", connection
                    );
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
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
        public ProductModelEntity GetProductByName(string name)
        {

            var connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM Product WHERE Product.Name='{name}';", connection
                    );
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
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
        public void CreateNewProduct(ProductModelEntity product)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            var connection = new SqlConnection(connectionString);
            using (connection)
            {
                string sql = $"insert into Product (Name,Price) values('{product.Name}',{product.Price})";

                connection.Open();

                adapter.InsertCommand = new SqlCommand(sql, connection);
                adapter.InsertCommand.ExecuteNonQuery();

                connection.Close();
            }
        }
        public void CreateNewProducts(List<ProductModelEntity> product)
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
                adapter.InsertCommand.ExecuteNonQuery();

                connection.Close();
            }
        }
        public void EditProduct(ProductModelEntity product)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            var connection = new SqlConnection(connectionString);
            using (connection)
            {
                string sql = $"update Product set Name = '{product.Name}', Price={product.Price} where Id ={product.Id}";

                connection.Open();

                adapter.UpdateCommand = connection.CreateCommand();
                adapter.UpdateCommand.CommandText = sql;
                adapter.UpdateCommand.ExecuteNonQuery();

                connection.Close();
            }
        }
        public void DeleteProduct(int id)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            var connection = new SqlConnection(connectionString);
            using (connection)
            {
                string sql = $"delete Product where Id ={id}";

                connection.Open();

                adapter.DeleteCommand = connection.CreateCommand();
                adapter.DeleteCommand.CommandText = sql;
                adapter.DeleteCommand.ExecuteNonQuery();

                connection.Close();
            }
        }
        public bool CheckId(int id)
        {
            var connection = new SqlConnection(connectionString);
            bool checkId;
            using (connection)
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM Product WHERE Product.Id={id};", connection
                    );
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

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
