using System.Runtime.Serialization;

namespace Contracts.Exceptions;

public class NotPermissionsException : Exception
{
    public NotPermissionsException()
    {
    }

    protected NotPermissionsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NotPermissionsException(string? message) : base(message)
    {
    }

    public NotPermissionsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}