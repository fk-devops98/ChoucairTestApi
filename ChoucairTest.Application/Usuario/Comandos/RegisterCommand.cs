using ChoucairTest.Application.Usuario.Dtos;
using MediatR;

namespace ChoucairTest.Application.Usuario.Comandos;

public record RegisterCommand
(
    string Email,
    string Password
): IRequest<UsuarioDto>;
