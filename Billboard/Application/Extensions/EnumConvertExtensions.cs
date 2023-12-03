using Persistence.Enums;

namespace Application.Extensions;

public static class EnumConvertExtensions
{
    public static PaymentStatusId ConvertToEnum(this string paymentStatus)
    {
        var status = string.Concat(paymentStatus.Split('_'));
        return Enum.Parse<PaymentStatusId>(status, ignoreCase: true);
    }
}