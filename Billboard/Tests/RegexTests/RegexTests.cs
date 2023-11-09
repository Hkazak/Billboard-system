using Application.Regexes;
using Moq;
using NUnit.Framework;

namespace Tests.RegexTests;

public class RegexTests
{
    [TestCase("+7 777 777 77 77")]
    [TestCase("+7(777)777 77 77")]
    [TestCase("+7-777-777-77-77")]
    [TestCase("+7-777-777-77 77")]
    [TestCase("+7-777-777 77 77")]
    [TestCase("+7-777-777 77-77")]
    [TestCase("+7-777 777-77-77")]
    [TestCase("+7 777 777 77-77")]
    [TestCase("87777777777")]
    [TestCase("8 7777777777")]
    [TestCase("+77777777777")]
    public void PhoneRegex_ValidPhones_MatchTrue(string phone)
    {
        Assert.That(ValidationRegexes.PhoneNumberRegex().IsMatch(phone), Is.True);
    }
    
    [TestCase("+77 77 777 77 77")]
    [TestCase("+7-77-7777-77-77")]
    [TestCase("6-777-777-77 77")]
    [TestCase("+777-777 77 77")]
    [TestCase("+7-777-(777) 77-77")]
    [TestCase("+75-777 777-77-77")]
    [TestCase("+78 777 777 77-77")]
    [TestCase("+8 7777777777")]
    [TestCase("+777 7 7777777")]
    [TestCase("This is not phone number")]
    public void PhoneRegex_InvalidPhones_MatchFalse(string phone)
    {
        Assert.That(ValidationRegexes.PhoneNumberRegex().IsMatch(phone), Is.False);
    }

    [TestCase("P@ssw0rd")]
    [TestCase("Passw0rd")]
    [TestCase("Abcd12354")]
    public void PasswordRegex_ValidPasswords_MatchTrue(string password)
    {
        Assert.That(ValidationRegexes.Length8AtLeastOneCharAndDigitPasswordRegex().IsMatch(password), Is.True);
    }
    
    [TestCase("Pass123")]
    [TestCase("WeakPassword")]
    [TestCase("NoDigitsHere")]
    [TestCase("UpperCASE")]
    [TestCase("lowercaseonly")]
    [TestCase("ShortPW")]
    [TestCase("Special@Char")]
    [TestCase("missinguppercase123")]
    [TestCase("LongEnoughButNoDigits")]
    public void PasswordRegex_InvalidPasswords_MatchFalse(string password)
    {
        Assert.That(ValidationRegexes.Length8AtLeastOneCharAndDigitPasswordRegex().IsMatch(password), Is.False);
    }
}