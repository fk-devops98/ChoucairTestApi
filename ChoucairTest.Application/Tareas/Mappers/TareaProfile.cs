using AutoMapper;
using ChoucairTest.Application.Tareas.Dtos;
using ChoucairTest.Domain.Entities;

namespace ChoucairTest.Application.Tareas.Mappers;

public class TareaProfile : Profile
{
    public TareaProfile()
    {
        CreateMap<Tarea, TareaDto>().ReverseMap();
    }
}
