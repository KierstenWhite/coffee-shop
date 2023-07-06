using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using CoffeeShop.Models;


namespace CoffeeShop.Repositories
{
    public class CoffeeRepository : ICoffeeRepository
    {
        private readonly string _connectionString;
        public CoffeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private SqlConnection Connection
        {
            get { return new SqlConnection(_connectionString); }
        }

        public List<Coffee> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Title, BeanVarietyId FROM Coffee;";
                    var reader = cmd.ExecuteReader();
                    var drinks = new List<Coffee>();
                    while (reader.Read())
                    {
                        var drink = new Coffee()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            BeanVarietyId = reader.GetInt32(reader.GetOrdinal("BeanVarietyId"))
                        };
                        drinks.Add(drink);
                    }

                    reader.Close();
                    return drinks;
                }
            }
        }

        public Coffee Get(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT Id, Title, BeanVarietyId
                    FROM Coffee
                    WHERE Id = @id;
                    ";
                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();

                    Coffee drink = null;
                    if (reader.Read())
                    {
                        drink = new Coffee()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            BeanVarietyId = reader.GetInt32(reader.GetOrdinal("BeanVarietyId"))
                        };
                    }

                    reader.Close();
                    return drink;
                }
            }
        }

        public void Add(Coffee drink)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Coffee (Title, BeanVarietyId)
                        OUTPUT INSERTED.ID
                        VALUES (@title, @beanVarietyId);
                    ";
                    cmd.Parameters.AddWithValue("@title", drink.Title);
                    cmd.Parameters.AddWithValue("@beanVarietyId", drink.BeanVarietyId);
                    drink.Id = (int)cmd.ExecuteScalar();

                }
            }
        }

        public void Update(Coffee drink)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    UPDATE Coffee
                        SET Title = @title,
                            BeanVarietyId = @beanVarietyId
                        WHERE Id = @id
                    ";
                    cmd.Parameters.AddWithValue("@title", drink.Title);
                    cmd.Parameters.AddWithValue("@beanVarietyId", drink.BeanVarietyId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Coffee WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}
