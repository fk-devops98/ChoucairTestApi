using AutoMapper;
using ChoucairTest.Application.Tareas.Dtos;
using ChoucairTest.Domain.Entities;
using ChoucairTest.Domain.Services;
using MediatR;

namespace ChoucairTest.Application.Tareas.Comandos;

public class CreateHandler : IRequestHandler<CreateCommand, TareaDto>
{
    private readonly TareaService _tareaService;
    private readonly IMapper _mapper;
    public CreateHandler(TareaService tareaService, IMapper mapper)
    {
        _tareaService = tareaService;
        _mapper = mapper;
    }
    public async Task<TareaDto> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var tarea = new Tarea()
        {
            Titulo = request.Titulo,
            Descripcion = request.Descripcion,
            FechaVencimiento = request.FechaVencimiento,
        };

        var result = await _tareaService.GuardarTareaAsync(tarea);

        return _mapper.Map<TareaDto>(result);   
    }
}
