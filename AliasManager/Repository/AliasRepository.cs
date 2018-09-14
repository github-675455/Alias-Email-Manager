using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;
using AliasManager.Model;

namespace AliasManager.Repository
{
    public class AliasesRepository : IRepository<Aliases>
    {
        private string connectionString;

        public AliasesRepository(IConfiguration configuration)
        {
            connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public Aliases Add(Aliases item)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO aliases (alias,email,enabled) VALUES(@Alias,@Email,@Enabled) RETURNING Id";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("Alias", NpgsqlDbType.Text, item.Alias);
                    command.Parameters.AddWithValue("Email", NpgsqlDbType.Text, item.Email);
                    command.Parameters.AddWithValue("Enabled", NpgsqlDbType.Boolean, item.Enabled);
                    
                    item.Id = (long)command.ExecuteScalar();
                }
            }
            return item;
        }

        public IList<Aliases> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Aliases>("SELECT * FROM aliases").ToList();
            }
        }

        public Aliases FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Aliases>("SELECT * FROM aliases WHERE Id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public Aliases FindByAlias(String alias)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Aliases>("SELECT * FROM aliases WHERE alias = @Alias", new { Alias = alias }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM aliases WHERE Id=@Id", new { Id = id });
            }
        }

        public void Update(Aliases item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE aliases SET Alias = @Alias, Email = @Email, Enabled = @Enabled WHERE Id = @Id", item);
            }
        }
    }
}
