using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar o nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar o Telefone")]
        public int Telefone { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar o E-mail")]
        public string Email { get; set; }
    }
}