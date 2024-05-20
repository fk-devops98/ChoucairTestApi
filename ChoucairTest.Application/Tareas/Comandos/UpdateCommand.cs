using ChoucairTest.Application.Tareas.Dtos;
using MediatR;

namespace ChoucairTest.Application.Tareas.Comandos;

public record UpdateCommand
(
    Guid Id,
    string? Titulo,
    string? Descripcion,
    DateTime? FechaVencimiento
): IRequest<TareaDto>;
