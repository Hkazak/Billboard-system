using System.Text.RegularExpressions;

namespace Application.Regexes;

public partial class PasswordRegexes
{
    [GeneratedRegex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")]
    public static partial Regex Length8AtLeastOneCharAndDigitPasswordRegex();
}