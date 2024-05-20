namespace ChoucairTest.Domain.Exceptions;

[Serializable]
public class TareaNotFoundException : Exception
{
    public TareaNotFoundException()
    {
    }
    public TareaNotFoundException(string message) : base(message)
    {
    }

    public TareaNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected TareaNotFoundException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
