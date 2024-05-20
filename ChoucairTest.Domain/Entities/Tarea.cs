using ChoucairTest.Domain.Entities.Base;

namespace ChoucairTest.Domain.Entities;

public class Tarea : EntityBase<Guid>
{
    public string? Titulo { get; set; }
    public string? Descripcion { get; set; }
    public DateTime? FechaVencimiento { get; set; }
    public Guid? EstadoTareaId { get; set; }
    public bool? Eliminada { get; set; }
    public Guid? UsuarioId { get; set; } = default!;
    public virtual EstadoTarea? EstadoTarea { get; set; }

    /// <summary>
    /// Constructor por defecto
    /// </summary>
    public Tarea()
    {
        
    }

    /// <summary>
    /// Usado en pruebas unitarias
    /// </summary>
    /// <param name="id"></param>
    /// <param name="titulo"></param>
    /// <param name="descripcion"></param>
    /// <param name="fechaVencimiento"></param>
    /// <param name="estadoTareaId"></param>
    public Tarea(Guid id, string titulo, string descripcion, DateTime fechaVencimiento, Guid estadoTareaId)
    {
        Id = id;
        Titulo = titulo;
        Descripcion = descripcion;
        FechaVencimiento = fechaVencimiento;
        EstadoTareaId = estadoTareaId;
    }
}
