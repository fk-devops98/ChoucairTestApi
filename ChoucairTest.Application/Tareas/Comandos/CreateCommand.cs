using ChoucairTest.Application.Tareas.Dtos;
using MediatR;

namespace ChoucairTest.Application.Tareas.Comandos;

public record CreateCommand
(
    string? Titulo,
    string? Descripcion,
    DateTime? FechaVencimiento
): IRequest<TareaDto>;
