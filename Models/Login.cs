using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;
public class LoginViewModel
{
    [Required(ErrorMessage = "Campo obrigatório")]
    [EmailAddress(ErrorMessage = "Endereço de email inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    [DataType(DataType.Password)]
    public string Senha { get; set; }

    [Required(ErrorMessage = "Selecione o tipo de usuário")]
    public string TipoUsuario { get; set; }
}
