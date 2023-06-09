﻿using ApiAuthentication.Model;
using Dapper;
using LoginSystem.Models.Interfaces;
using System.Data.SqlClient;

namespace LoginSystem.Models.DAL
{
    public class UserDAL : IUserDAL
    {
        IConfiguration _configuration;
        public UserDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            return connection;
        }

        public int Insert(UserModel user)
        {
            var query = 0;
            var connectionString = this.GetConnection();
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    if (user.Name != null)
                    {
                        query = con.Execute("INSERT INTO Users (Name, Email, Password) Values(@Name, @Email, @Password)", new { Name = user.Name, Email = user.Email, Password = user.Password });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return query;
            }
        }

        public List<AuthContext> Update(UserModel user)
        {
            var query = new List<AuthContext>();
            var connectionString = this.GetConnection();
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    if (user.Email != null)
                    {
                        query = con.Query<AuthContext>("SELECT * FROM Users WHERE Email = @Email", param: new { Email = user.Email }).ToList();

                        if (query.Count > 0)
                        {
                            con.Execute("UPDATE Users SET Password = @Password WHERE Email = @Email ", new { Password = user.Password, Email = user.Email });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return query;
            }
        }
    }
}
