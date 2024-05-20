using ChoucairTest.Domain.Entities;
using ChoucairTest.Domain.Exceptions;
using ChoucairTest.Domain.Ports;
using ChoucairTest.Domain.Services.Base;
using System.Threading;

namespace ChoucairTest.Domain.Services;

[DomainService]
public class TareaService
{
    private const string CODIGO_ESTADO_TAREA_CREADA = "C";

    private readonly ITareaRepository _tareaRepository;
    private readonly IEstadoTareaRepository _estadoTareaRepository;
    private readonly ILoginMiddleware _loginMiddleware;
    public TareaService(ITareaRepository tareaRepository, IEstadoTareaRepository estadoTareaRepository, ILoginMiddleware loginMiddleware)
    {
        _tareaRepository = tareaRepository;
        _estadoTareaRepository = estadoTareaRepository;
        _loginMiddleware = loginMiddleware;
    }

    public async Task<Tarea> GuardarTareaAsync(Tarea tarea) 
    {
        var userId = _loginMiddleware.GetIdUserLoggerd();

        var estadoTarea = (await _estadoTareaRepository.GetAsync(e=> e.Codigo == CODIGO_ESTADO_TAREA_CREADA)).FirstOrDefault();

        if (estadoTarea != null)
        {
            tarea.EstadoTareaId = estadoTarea.Id; 
        }

        tarea.UsuarioId = userId;
        tarea.Eliminada = false;

        return await _tareaRepository.AddAsync(tarea);
    }

    public async Task<Tarea> EditarTareaAsync(Tarea tarea)
    {
        var entity = await _tareaRepository.GetByIdAsync(tarea.Id) ?? throw new TareaNotFoundException($"No se encontro una tarea con id: {tarea.Id}");

        entity.Titulo = tarea.Titulo;
        entity.Descripcion = tarea.Descripcion;
        entity.FechaVencimiento = tarea.FechaVencimiento;

        return await _tareaRepository.UpdateAsync(entity);
    }

    public async Task EliminarTareaAsync(Guid tareaId) 
    {
        var entity = await _tareaRepository.GetByIdAsync(tareaId) ?? throw new TareaNotFoundException($"No se encontro una tarea con id: {tareaId}");

        entity.Eliminada = true;

        await _tareaRepository.UpdateAsync(entity);
    }

    public async Task CambiarEstadoTareaAsync(Guid tareaId, string codigoEstado) 
    {
        var entity = await _tareaRepository.GetByIdAsync(tareaId) ?? throw new TareaNotFoundException($"No se encontro una tarea con id: {tareaId}");

        var estadoTarea = (await _estadoTareaRepository.GetAsync(e => e.Codigo == codigoEstado)).FirstOrDefault() ?? 
            throw new EstadoNotRegisteredException($"No se encontro un estado con codigo: {codigoEstado}");
        
        entity.EstadoTareaId = estadoTarea.Id;
        
        await _tareaRepository.UpdateAsync(entity);
    }
}
