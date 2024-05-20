namespace ChoucairTest.Domain.Test.Tarea;

public class TareaDataBuilder
{
    Guid Id = default!;
    string Titulo = default!;
    string Descripcion = default!;
    DateTime FechaVencimiento = default!;
    Guid EstadoTareaId = default!;

    public Domain.Entities.Tarea Build() 
    {
        return new(Id, Titulo, Descripcion, FechaVencimiento, EstadoTareaId);
    }
    public TareaDataBuilder WithId(Guid id)
    {
        Id = id;
        return this;
    }

    public TareaDataBuilder WithTitulo(string titulo)
    {
        Titulo = titulo;
        return this;
    }

    public TareaDataBuilder WithDescripcion(string descripcion)
    {
        Descripcion = descripcion;
        return this;
    }

    public TareaDataBuilder WithFechaVencimiento(DateTime fechaVencimiento)
    {
        FechaVencimiento = fechaVencimiento;
        return this;
    }
}
