using ChoucairTest.Application.Tareas.Dtos;
using MediatR;

namespace ChoucairTest.Application.Tareas.Consultas;

public record OneQuery
(
    Guid Id
) : IRequest<TareaDto>;
