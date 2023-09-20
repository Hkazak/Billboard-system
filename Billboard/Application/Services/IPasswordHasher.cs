namespace Application.Services;

public interface IPasswordHasher
{
    // TODO add implementation to calculate hash by SHA256
    string CalculateHash(string password);
}