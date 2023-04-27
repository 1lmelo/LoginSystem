using ApiAuthentication.Model;

namespace ApiAuthentication.Interfaces
{
    public interface IAuthAdapter
    {
        AuthResponse AuthenticationAdapter(List<AuthContext> user);
    }
}
