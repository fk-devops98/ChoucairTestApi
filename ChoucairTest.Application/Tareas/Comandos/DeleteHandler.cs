using ChoucairTest.Domain.Services;
using MediatR;

namespace ChoucairTest.Application.Tareas.Comandos;

public class DeleteHandler : AsyncRequestHandler<DeleteCommand>
{
    private readonly TareaService _tareaService;
    public DeleteHandler(TareaService tareaService)
    {
        _tareaService = tareaService;
    }
    
    protected override async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        await _tareaService.EliminarTareaAsync(request.Id);
    }
}
