using Application.ServicesImplementations;
using NUnit.Framework;

namespace Tests.ServicesTests;

public class PasswordHasherTests
{
    [Test]
    public void CalculateHash_Psswrd_185a9d48f24c1cc0886d39a1526ee9c8656a5dc39876496cb71e27ecd4ceb11d()
    {
        const string expected = "185a9d48f24c1cc0886d39a1526ee9c8656a5dc39876496cb71e27ecd4ceb11d";
        var hasher = new PasswordHasher();
        var actual = hasher.CalculateHash("Psswrd");
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void CalculateHash_Pssw0rd_185a9d48f24c1cc0886d39a1526ee9c8656a5dc39876496cb71e27ecd4ceb11d()
    {
        const string expected = "17cefeb007fe23e1adf12545b45e65e278255efcfa8facd3309aae5a545e40be";
        var hasher = new PasswordHasher();
        var actual = hasher.CalculateHash("Pssw0rd");
        Assert.That(actual, Is.EqualTo(expected));
    }
}