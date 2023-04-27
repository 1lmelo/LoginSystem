using ApiAuthentication.Model;

namespace ApiAuthentication.Interfaces
{
    public interface IAuthRepository
    {
        List<AuthContext> GetUser(AuthRequest request);
    }
}
