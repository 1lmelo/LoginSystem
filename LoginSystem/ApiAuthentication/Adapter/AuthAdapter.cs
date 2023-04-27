using ApiAuthentication.Enum;
using ApiAuthentication.Interfaces;
using ApiAuthentication.Model;
using System.Net;

namespace ApiAuthentication.Adapter
{
    public class AuthAdapter : IAuthAdapter
    {

        public AuthResponse AuthenticationAdapter(List<AuthContext> user)
        {
            try
            {
                var response = new AuthResponse();
                if (user.FirstOrDefault().Email != null)
                {
                    response.Email = user.FirstOrDefault().Email;
                    response.Name = user.FirstOrDefault().Name;
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
