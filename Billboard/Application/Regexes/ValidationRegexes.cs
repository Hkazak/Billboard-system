using System.Text.RegularExpressions;

namespace Application.Regexes;

public partial class ValidationRegexes
{
    [GeneratedRegex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")]
    public static partial Regex Length8AtLeastOneCharAndDigitPasswordRegex();
    
    [GeneratedRegex(@"/\+?\d{1,4}?[-.\s]?\(?\d{1,3}?\)?[-.\s]?\d{1,4}[-.\s]?\d{1,4}[-.\s]?\d{1,2}[-.\s]?\d{1,2}$")]
    public static partial Regex PhoneNumberRegex();
}
