namespace sysvendas2.Models;

public class Pedido
{
    public Pedido(int idPedido, DateTime data, Cliente cliente, string status)
    {
        IdPedido = idPedido;
        Data = data;
        Cliente = cliente;
        IdCliente = cliente.IdCliente;
        Status = status;
        Items = new List<ItemPedido>();
    }

    public int IdPedido { get; set; }
    public DateTime Data { get; set; }
    public int IdCliente { get; set; } // necessário para banco de dados
    public Cliente Cliente { get; set; } // útil para exibição, mas não salvo diretamente no banco
    public string Status { get; set; }
    public double Total { get; set; }
    public List<ItemPedido> Items { get; set; }
}
