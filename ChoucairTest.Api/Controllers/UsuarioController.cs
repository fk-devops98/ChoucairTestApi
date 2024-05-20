using ChoucairTest.Api.Controllers.Base;
using ChoucairTest.Application.Usuario.Comandos;
using ChoucairTest.Application.Usuario.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChoucairTest.Api.Controllers;

[AllowAnonymous]
public class UsuarioController : BaseController
{
    public UsuarioController(IMediator mediator) 
        : base(mediator)
    {
    }

    [HttpPost]
    public async Task<UsuarioDto> Post(RegisterCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPost("login")]
    public async Task<UsuarioLoggedDto> Login(LoginCommand command)
    {
        return await _mediator.Send(command);
    }
}
