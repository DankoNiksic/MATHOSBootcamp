﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCRUD.Model;
using WebCRUD.Repository.Common;

namespace WebCRUD.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        static string connectionString = @"Data Source=localhost\SQLEXPRESS01;Initial Catalog=WebAPI_CRUD;Integrated Security=True";
                
        public async Task<List<CustomerModelEntity>> GetAllCustomerAsync()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            List<CustomerModelEntity> listCustomers = new List<CustomerModelEntity>();
            using (connection)
            {
                SqlCommand command = new SqlCommand(
                    "SELECT * FROM Customer;", connection
                    );
                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CustomerModelEntity model = new CustomerModelEntity
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Address = reader["Address"].ToString(),
                            Phone = (int)reader["Phone"]
                        };
                        listCustomers.Add(model);
                    }
                }
                reader.Close();
                connection.Close();
            }
            return listCustomers;
        }
        public async Task<CustomerModelEntity> GetCustomerByIdAsync(int id)
        {

            var connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM Customer WHERE Customer.Id={id};", connection
                    );
                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();
                CustomerModelEntity model = new CustomerModelEntity();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        model.Id = (int)reader["Id"];
                        model.Name = reader["Name"].ToString();
                        model.Email = reader["Email"].ToString();
                        model.Address = reader["Address"].ToString();
                        model.Phone = (int)reader["Phone"];
                    }
                }
                reader.Close();
                connection.Close();
                return model;
            }
        }
        public async Task<CustomerModelEntity> GetCustomerByNameAsync(string name)
        {

            var connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM Customer WHERE Customer.Name='{name}';", connection
                    );
                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();
                CustomerModelEntity model = new CustomerModelEntity();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        model.Id = (int)reader["Id"];
                        model.Name = reader["Name"].ToString();
                        model.Email = reader["Email"].ToString();
                        model.Address = reader["Address"].ToString();
                        model.Phone = (int)reader["Phone"];
                    }
                }
                reader.Close();
                connection.Close();
                return model;
            }
        }
        public async Task CreateNewCustomerAsync(CustomerModelEntity customer)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            var connection = new SqlConnection(connectionString);
            using (connection)
            {
                string sql = $"insert into Customer (Name,Email,Address,Phone) values('{customer.Name}'," +
                    $"'{customer.Email}','{customer.Address}',{customer.Phone});";

                connection.Open();

                adapter.InsertCommand = new SqlCommand(sql, connection);
               await adapter.InsertCommand.ExecuteNonQueryAsync();

                connection.Close();
            }
        }
        public async Task CreateNewCustomersAsync(List<CustomerModelEntity> customer)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            var connection = new SqlConnection(connectionString);
            using (connection)
            {
                string sql = "insert into Customer (Name,Email,Address,Phone) values";
                foreach (var item in customer)
                {
                    sql += $" ('{item.Name}','{item.Email}','{item.Address}'," +
                        $"{item.Phone}),";
                }
                sql = sql.Remove(sql.Length - 1, 1) + ";";
                connection.Open();

                adapter.InsertCommand = new SqlCommand(sql, connection);
               await adapter.InsertCommand.ExecuteNonQueryAsync();

                connection.Close();
            }
        }
        public async Task EditCustomerAsync(CustomerModelEntity customer)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            var connection = new SqlConnection(connectionString);
            using (connection)
            {
                string sql = $"update Customer set Name = '{customer.Name}'," +
                    $" Email='{customer.Email}',Address='{customer.Address}'," +
                    $"Phone={customer.Phone} where Id ={customer.Id}";

                connection.Open();

                adapter.UpdateCommand = connection.CreateCommand();
                adapter.UpdateCommand.CommandText = sql;
              await adapter.UpdateCommand.ExecuteNonQueryAsync();

                connection.Close();
            }
        }
        public async Task DeleteCustomerAsync(int id)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            var connection = new SqlConnection(connectionString);
            using (connection)
            {
                string sql = $"delete Customer where Id ={id}";

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
                    $"SELECT * FROM Customer WHERE Customer.Id={id};", connection
                    );
                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();

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
