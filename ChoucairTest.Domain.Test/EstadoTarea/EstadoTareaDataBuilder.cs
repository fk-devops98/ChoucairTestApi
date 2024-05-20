namespace ChoucairTest.Domain.Test.EstadoTarea;

public class EstadoTareaDataBuilder
{
    Guid Id = default!;
    string Codigo = default!;
    string Nombre = default!;

    public Domain.Entities.EstadoTarea Build() 
    {
        return new Domain.Entities.EstadoTarea(Id, Codigo, Nombre);
    }

    public List<Domain.Entities.EstadoTarea> BuildList()
    {
        return new List<Domain.Entities.EstadoTarea>() { Build() };
    }

    public EstadoTareaDataBuilder WithId(Guid id) 
    {
        Id = id;
        return this;
    }
    public EstadoTareaDataBuilder WithCodigo(string codigo)
    {
        Codigo = codigo;
        return this;
    }
    public EstadoTareaDataBuilder WithNombre(string nombre)
    {
        Nombre = nombre;
        return this;
    }
}
