using ChoucairTest.Api.Controllers.Base;
using ChoucairTest.Application.Tareas.Comandos;
using ChoucairTest.Application.Tareas.Consultas;
using ChoucairTest.Application.Tareas.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChoucairTest.Api.Controllers;

[Authorize]
public class TareaController : BaseController
{
    public TareaController(IMediator mediator) 
        : base(mediator)
    {
    }

    [HttpGet]
    public async Task<List<TareaGridDto>> Get()
        => await _mediator.Send(new AllQuery());

    [HttpGet("{id}")]
    public async Task<TareaDto> Get(Guid id)
        => await _mediator.Send(new OneQuery(id));

    [HttpPost]
    public async Task<TareaDto> Post(CreateCommand command) 
        => await _mediator.Send(command);
    
    [HttpPut("{id}")]
    public async Task<TareaDto> Put(UpdateCommand command)
        => await _mediator.Send(command);

    [HttpDelete("{id}")]
    public async Task Delete(Guid id) 
        => await _mediator.Send(new DeleteCommand(id));

    [HttpPut("{id}/estado/{codigo}")]
    public async Task PutEstado(Guid id, string codigo)
        => await _mediator.Send(new UpdateEstadoCommand(id, codigo));
}
