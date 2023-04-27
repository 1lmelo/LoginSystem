using ApiAuthentication.Model;

namespace LoginSystem.Models.Interfaces
{
    public interface IUserDAL
    {
        int Insert(UserModel user);
        List<AuthContext> Update(UserModel user);
    }
}
