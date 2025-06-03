namespace sysvendas2.Interfaces;
using sysvendas2.Models;

public interface IRepositorioProduto
{
    void Adicionar(Produto produto);
    List<Produto> ObterTodos(); 
    Produto? ObterProduto(string sku);
}