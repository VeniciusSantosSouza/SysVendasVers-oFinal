namespace sysvendas2.Models;

public class ItemPedido
{
    public int IdItemPedido { get; set; }     // PK da tabela
    public int IdPedido { get; set; }         // FK para Pedido
    public int IdProduto { get; set; }        // FK para Produto
    public int Quantidade { get; set; }

    public Produto? Produto { get; set;}
    public int Desconto { get; set; }         // em porcentagem (%), exemplo: 10 para 10%
    public double Preco { get; set; }         // Preço unitário SEM desconto
    public double SubTotal { get; set; }      // Preço final com desconto * quantidade
}
