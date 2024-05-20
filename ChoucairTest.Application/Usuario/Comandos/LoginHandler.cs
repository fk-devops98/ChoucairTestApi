using AutoMapper;
using ChoucairTest.Application.Usuario.Dtos;
using ChoucairTest.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChoucairTest.Application.Usuario.Comandos;

public class LoginHandler : IRequestHandler<LoginCommand, UsuarioLoggedDto>
{
    private readonly SignInManager<Domain.Entities.Usuario> _signInManager;
    private readonly UserManager<Domain.Entities.Usuario> _userManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public LoginHandler(SignInManager<Domain.Entities.Usuario> signInManager, UserManager<Domain.Entities.Usuario> userManager, IMapper mapper, IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<UsuarioLoggedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);

        if (!result.Succeeded) throw new UserUnregisteredException("Usuario inexistente");

        var user = await _userManager.FindByEmailAsync(request.Email);

        var userLogged = _mapper.Map<UsuarioLoggedDto>(user);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Sid, user.Id),
            new(ClaimTypes.Name, user.UserName),
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(720),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        userLogged.AccesToken = jwt;

        return userLogged;
    }
}
