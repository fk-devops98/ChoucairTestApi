using AutoMapper;
using ChoucairTest.Application.Tareas.Dtos;
using ChoucairTest.Domain.Entities;
using ChoucairTest.Domain.Services;
using MediatR;

namespace ChoucairTest.Application.Tareas.Comandos;

public class UpdateHandler : IRequestHandler<UpdateCommand, TareaDto>
{
    private readonly TareaService _tareaService;
    private readonly IMapper _mapper;
    public UpdateHandler(TareaService tareaService, IMapper mapper)
    {
        _tareaService = tareaService;
        _mapper = mapper;
    }
    public async Task<TareaDto> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var tarea = new Tarea()
        {
            Id = request.Id,
            Titulo = request.Titulo,
            Descripcion = request.Descripcion,
            FechaVencimiento = request.FechaVencimiento,
        };

        var result = await _tareaService.EditarTareaAsync(tarea);

        return _mapper.Map<TareaDto>(result);   
    }
}
