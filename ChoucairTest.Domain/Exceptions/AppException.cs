namespace ChoucairTest.Domain.Exceptions;

[Serializable]
public class AppException : Exception
{
    public AppException()
    { }

    public AppException(string message) : base(message)
    {
    }

    public AppException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected AppException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}