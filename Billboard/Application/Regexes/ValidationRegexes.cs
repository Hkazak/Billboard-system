using System.Text.RegularExpressions;

namespace Application.Regexes;

public partial class ValidationRegexes
{
    [GeneratedRegex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")]
    public static partial Regex Length8AtLeastOneCharAndDigitPasswordRegex();
    
    [GeneratedRegex(@"^(\+7|7|8)?[\s\-]?\(?[489][0-9]{2}\)?[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2}$")]
    public static partial Regex PhoneNumberRegex();
}
