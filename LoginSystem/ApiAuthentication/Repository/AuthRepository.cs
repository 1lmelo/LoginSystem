using ApiAuthentication.Interfaces;
using ApiAuthentication.Model;
using Dapper;
using System.Data.SqlClient;

namespace ApiAuthentication.Repository
{
    public class AuthRepository : IAuthRepository
    {
        IConfiguration _configuration;

        public AuthRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            return connection;
        }

        public List<AuthContext> GetUser(AuthRequest request)
        {
            var query = new List<AuthContext>();
            var connectionString = this.GetConnection();
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    if (request.Email != null)
                    {
                        query = con.Query<AuthContext>("SELECT * FROM Users WHERE Email = @Email AND PASSWORD = @Password", param: new { Email = request.Email, Password = request.Password }).ToList();
              
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
