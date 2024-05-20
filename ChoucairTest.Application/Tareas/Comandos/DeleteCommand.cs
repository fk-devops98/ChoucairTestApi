using ChoucairTest.Application.Tareas.Dtos;
using MediatR;

namespace ChoucairTest.Application.Tareas.Comandos;

public record DeleteCommand
(
    Guid Id
): IRequest;
