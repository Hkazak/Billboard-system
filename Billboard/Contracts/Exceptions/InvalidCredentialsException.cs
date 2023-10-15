using System.Runtime.Serialization;

namespace Contracts.Exceptions;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException()
    {
    }

    protected InvalidCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidCredentialsException(string? message) : base(message)
    {
    }

    public InvalidCredentialsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}