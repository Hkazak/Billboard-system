using System.Runtime.Serialization;

namespace Contracts.Exceptions;

public class InternalServiceException : Exception
{
    public InternalServiceException()
    {
    }

    protected InternalServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InternalServiceException(string? message) : base(message)
    {
    }

    public InternalServiceException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}