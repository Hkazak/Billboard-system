using System.Runtime.Serialization;

namespace Contracts.Exceptions;

public class InvalidRequestDataException : Exception
{
    public InvalidRequestDataException()
    {
    }

    protected InvalidRequestDataException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidRequestDataException(string? message) : base(message)
    {
    }

    public InvalidRequestDataException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}