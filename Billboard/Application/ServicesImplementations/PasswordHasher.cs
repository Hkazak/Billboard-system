using System.Security.Cryptography;
using System.Text;
using Application.Services;

namespace Application.ServicesImplementations;

public class PasswordHasher : IPasswordHasher
{
    public string CalculateHash(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = SHA256.HashData(bytes);

        var stringBuilder = new StringBuilder();
        foreach (var data in hash)
        {
            stringBuilder.Append(data.ToString("x2"));
        }

        return stringBuilder.ToString();
    }
}