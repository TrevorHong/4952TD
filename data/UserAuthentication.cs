using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

public class UserAuthentication
{
    private ApplicationDbContext _context;

    public UserAuthentication(ApplicationDbContext context)
    {
        _context = context;
    }

    public bool Register(string username, string password, string email)
    {
        try
    {
        string hashedPassword = HashPassword(password);

        var user = new User { Username = username, Password = hashedPassword, Email = email };

        _context.Users.Add(user);

        int result = _context.SaveChanges();

        return result > 0;
    }
    catch (Exception ex)
    {
        // Log the exception message
        Console.WriteLine(ex.Message);
        return false;
    }
    }

    public bool IsUserExists(string username)
    {
        return _context.Users.Any(u => u.Username == username);
    }

    public bool Login(string username, string password)
    {
        string hashedPassword = HashPassword(password);

        var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);

        return user != null;
    }

    private string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}