using AutoMapper;
using ChoucairTest.Application.Usuario.Dtos;
using ChoucairTest.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ChoucairTest.Application.Usuario.Comandos;

public class RegisterHandler : IRequestHandler<RegisterCommand, UsuarioDto>
{
    private readonly UserManager<Domain.Entities.Usuario> _userManager;
    private readonly IMapper _mapper;

    public RegisterHandler(UserManager<Domain.Entities.Usuario> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<UsuarioDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new Domain.Entities.Usuario
        {
            UserName = request.Email,
            Email = request.Email,
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        var errorsMsg = string.Join(",",result.Errors.Select(e => e.Description));

        if (!result.Succeeded) throw new AppException($"errores: {errorsMsg}"); 

        return _mapper.Map<UsuarioDto>(user);
    }
}
