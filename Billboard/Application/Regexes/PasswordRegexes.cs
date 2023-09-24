using System.Text.RegularExpressions;

namespace Application.Regexes;

public partial class PasswordRegexes
{
    [GeneratedRegex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$")]
    public static partial Regex Length8AtLeastOneCharAndDigitPasswordRegex();
}