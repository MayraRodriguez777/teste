namespace WebApplication2.Models;
public class AgendaEvento
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public int UsuarioClienteId { get; set; }
    public int UsuarioDentistaId { get; set; }

    // Relacionamentos de chave estrangeira
    public UsuarioCliente UsuarioCliente { get; set; }
    public UsuarioDentista UsuarioDentista { get; set; }
}
