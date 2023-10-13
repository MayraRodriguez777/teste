using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    [Table("UsuarioCliente")]
    public class UsuarioCliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Telefone")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a Senha")]
        public string Senha { get; set; }

        // Propriedade de navegação para AgendaEventos
        public ICollection<AgendaEvento> AgendaEventos { get; set; }
    }
}
