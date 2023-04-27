using ApiAuthentication.Interfaces;
using ApiAuthentication.Model;
using System.Net;

namespace ApiAuthentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IAuthAdapter _adapter;
        public AuthService(IAuthRepository authRepository, IAuthAdapter adapter)
        {
            _authRepository = authRepository;
            _adapter = adapter;
        }

        public AuthResponse GetLoginUser(AuthRequest request)
        {
            try
            {
                var response = new AuthResponse();
                var verifyUser = _authRepository.GetUser(request);

                if (verifyUser.Count > 0)
                {
                    response = _adapter.AuthenticationAdapter(verifyUser);
                    var token = TokenService.GenerateToken(response);
                    response.Token = token;
                }
                else
                {
                    response.StatusCode = HttpStatusCode.BadRequest.ToString();
                    response.Message = "Not user found";
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
