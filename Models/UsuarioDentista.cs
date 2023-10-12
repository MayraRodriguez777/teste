using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    [Table("UsuarioDentista")]
    public class UsuarioDentista
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar o nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar o Telefone")]
        public int Telefone { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar o E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar o CRO")]
        public int CRO { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar o nome da clinica")]
        public string Nome_da_clinica { get; set; }
        [Required(ErrorMessage = "Obrigatorio informar o Endereço")]
        public string Endereço { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar a Senha")]
        public string Senha { get; set; }
    }
}
