using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    [Table("UsuarioCliente")]
    public class UsuarioCliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar o nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar o Telefone")]
        public int Telefone { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar o E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar a Senha")]
        public string Senha { get; set; }
    }
}