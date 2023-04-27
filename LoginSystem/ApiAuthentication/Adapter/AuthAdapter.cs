using ApiAuthentication.Enum;
using ApiAuthentication.Interfaces;
using ApiAuthentication.Model;
using System.Net;

namespace ApiAuthentication.Adapter
{
    public class AuthAdapter : IAuthAdapter
    {

        public AuthResponse AuthenticationAdapter(string email)
        {
            try
            {
                var response = new AuthResponse();
                if (email != null)
                {
                    response.Email = email;
                    response.StatusCode = HttpStatusCode.OK.ToString();
                    response.Message = "User found in database";
                    response.Role = RolesEnum.Manager.ToString();
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
