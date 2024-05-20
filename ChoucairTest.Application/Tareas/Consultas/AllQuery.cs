using ChoucairTest.Application.Tareas.Dtos;
using MediatR;

namespace ChoucairTest.Application.Tareas.Consultas;

public record AllQuery
() : IRequest<List<TareaGridDto>>;
