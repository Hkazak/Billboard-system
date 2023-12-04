using System.Runtime.Serialization;

namespace Contracts.Exceptions;

public class DataConflictException : Exception
{
    public DataConflictException()
    {
    }

    protected DataConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DataConflictException(string? message) : base(message)
    {
    }

    public DataConflictException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}