namespace ChoucairTest.Domain.Exceptions;

[Serializable]
public class EstadoNotRegisteredException : AppException
{
    public EstadoNotRegisteredException()
    {
    }
    public EstadoNotRegisteredException(string message) : base(message)
    {
    }

    public EstadoNotRegisteredException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected EstadoNotRegisteredException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
