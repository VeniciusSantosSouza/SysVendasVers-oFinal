namespace sysvendas2.Context;
using sysvendas2.Interfaces;

public static class DBContext
{
    public static IRepositorioCliente? RepositorioClientes { get; set; }
    public static IRepositorioProduto? RepositorioProdutos { get; set; }
    public static IRepositorioPedido? RepositorioPedidos { get; set; }
    public static IRepositorioItemPedido? RepositorioItemPedidos {get; set;}

}