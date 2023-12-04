using Contracts.Constants;

namespace Application.Extensions;

public static class FormatExtensions
{
    public static DateTime ToDate(this string dateString)
    {
        return DateTime.ParseExact(dateString, FormatConstants.ValidDateFormat, null);
    }

    public static DateTime ToDateIoka(this string dateString)
    {
        return DateTime.ParseExact(dateString, FormatConstants.ValidIokaDateTimeFormat, null);
    }
}