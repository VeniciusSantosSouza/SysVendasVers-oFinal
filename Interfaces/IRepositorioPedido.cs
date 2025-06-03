using sysvendas2.Models;

namespace sysvendas2.Interfaces
{
    public interface IRepositorioPedido
    {
        void CriarTabelas();
        Pedido? Adicionar(Pedido pedido);
        List<Pedido> ObterTodos();
    }
}
