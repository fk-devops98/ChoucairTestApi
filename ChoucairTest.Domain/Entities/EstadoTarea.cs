using ChoucairTest.Domain.Entities.Base;

namespace ChoucairTest.Domain.Entities;

public class EstadoTarea : EntityBase<Guid>
{
    public string? Codigo { get; set; }
    public string? Nombre { get; set; }
    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
    /// <summary>
    /// Constructor por defecto
    /// </summary>
    public EstadoTarea()
    {
        
    }

    /// <summary>
    /// Usado en pruebas unitarias
    /// </summary>
    /// <param name="codigo"></param>
    /// <param name="nombre"></param>
    public EstadoTarea(Guid id, string codigo, string? nombre)
    {
        Id = id;
        Codigo = codigo;
        Nombre = nombre;
    }
}
