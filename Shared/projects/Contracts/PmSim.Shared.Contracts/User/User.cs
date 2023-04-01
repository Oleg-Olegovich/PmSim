namespace PmSim.Shared.Contracts.User;

public class User
{
    public string Email { get; }
    
    public string Login { get; }
    
    public string Password { get; }
    
    public User(string email, string login, string password)
    {
        Email = email;
        Login = login;
        Password = password;
    }
}