namespace ChoucairTest.Domain.Exceptions;

[Serializable]
public class UserUnregisteredException : Exception
{
    public UserUnregisteredException()
    {
    }
    public UserUnregisteredException(string message) : base(message)
    {
    }

    public UserUnregisteredException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected UserUnregisteredException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
