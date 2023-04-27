using ApiAuthentication.Model;

namespace ApiAuthentication.Interfaces
{
    public interface IAuthRepository
    {
        bool GetUser(AuthRequest request);
    }
}
