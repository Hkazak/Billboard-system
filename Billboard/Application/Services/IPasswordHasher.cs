namespace Application.Services;

public interface IPasswordHasher
{
    string CalculateHash(string password);
}