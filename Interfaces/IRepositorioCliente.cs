namespace sysvendas2.Interfaces;
using sysvendas2.Models;

public interface IRepositorioCliente
{
    void Adicionar(Cliente cliente);
    List<Cliente> ObterTodos(); 
    Cliente? ObterClienteId(int idcliente);
    Cliente? ObterClienteNome(string nomecliente);

}