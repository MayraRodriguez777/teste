using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    [Table("UsuarioDentista")]
    public class UsuarioDentista
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Telefone")]
        public int Telefone { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o CRO")]
        public int CRO { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a Especialidade")]
        public string Especialidade { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o nome da clínica")]
        public string Nome_da_clinica { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Endereço")]
        public string Endereço { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a Senha")]
        public string Senha { get; set; }

        // Propriedade para armazenar a foto
        public byte[] Foto { get; set; }

        public ICollection<AgendaEvento> AgendaEventos { get; set; }
    }
}


