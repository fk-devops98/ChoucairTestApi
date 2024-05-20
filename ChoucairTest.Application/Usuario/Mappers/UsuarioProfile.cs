using AutoMapper;
using ChoucairTest.Application.Usuario.Dtos;

namespace ChoucairTest.Application.Usuario.Mappers;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<UsuarioDto, Domain.Entities.Usuario>().ReverseMap();
        CreateMap<UsuarioLoggedDto, Domain.Entities.Usuario>().ReverseMap();
    }
}
