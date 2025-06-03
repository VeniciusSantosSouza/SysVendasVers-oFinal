namespace sysvendas2.Models;

public class Cliente
{
    public Cliente(int idCliente, string nome, string email, string telefone)
    {
        IdCliente = idCliente;
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }
    
    public int IdCliente { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    
}