using MediatR;

namespace ChoucairTest.Application.Tareas.Comandos;

public record UpdateEstadoCommand
(
    Guid Id,
    string CodigoEstado
): IRequest;
