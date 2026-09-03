using JambOfficeApp.Data;
using JambOfficeApp.Models;

namespace JambOfficeApp.Services;

public class AuthService
{
    private readonly AppDbContext _db = new AppDbContext();

    public User Register(string username, string password, string role)
    {
        bool usernameTaken = _db.Users.Any(u => u.Username == username);
        if (usernameTaken) return null;

        var user = new User
        {
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = role
        };
        
        _db.Users.Add(user);
        _db.SaveChanges();
        return user;
    }
    
    public User Login(string username, string password)
    {
        var user = _db.Users.FirstOrDefault(u => u.Username == username);
        if (user == null) return null;

        bool passwordMatches = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        return passwordMatches ? user : null;
    }

    public bool NoUsersExist()
    {
        return !_db.Users.Any();
    }
}