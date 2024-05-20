namespace ChoucairTest.Application.Tareas.Dtos;

public class TareaDto
{
    public Guid Id { get; set; }
    public string? Titulo { get; set; }
    public string? Descripcion { get; set; }
    public DateTime? FechaVencimiento { get; set; }
    public Guid? EstadoTareaId { get; set; }
    public Guid? UsuarioId { get; set; }
    public bool? Eliminada { get; set; }
}
