using ApiAuthentication.Model;

namespace ApiAuthentication.Interfaces
{
    public interface IAuthService
    {
        AuthResponse GetLoginUser(AuthRequest request);
    }
}
