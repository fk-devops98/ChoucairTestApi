using ChoucairTest.Domain.Services;
using MediatR;

namespace ChoucairTest.Application.Tareas.Comandos;

public class UpdateEstadoHandler : AsyncRequestHandler<UpdateEstadoCommand>
{
    private readonly TareaService _tareaService;
    public UpdateEstadoHandler(TareaService tareaService)
    {
        _tareaService = tareaService;
    }
    protected override async Task Handle(UpdateEstadoCommand request, CancellationToken cancellationToken)
    {
        await _tareaService.CambiarEstadoTareaAsync(request.Id, request.CodigoEstado);
    }
}
