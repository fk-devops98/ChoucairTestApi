namespace ChoucairTest.Application.Usuario.Dtos;

public class UsuarioLoggedDto 
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string AccesToken { get; set; }
}
