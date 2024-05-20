using ChoucairTest.Application.Usuario.Dtos;
using MediatR;

namespace ChoucairTest.Application.Usuario.Comandos;

public record LoginCommand
(
    string Email,
    string Password
) : IRequest<UsuarioLoggedDto>;
