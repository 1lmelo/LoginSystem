namespace LoginSystem.Models.Interfaces
{
    public interface IEmailDAL
    {
        Task EmailCreateUser(string email, string name);
    }
}
