namespace sysvendas2.Models;

public class Produto
{
    public int IdProduto { get; set; }      // Será usado como PK no banco (SERIAL / IDENTITY)
    public string Sku { get; set; } = string.Empty;       // Código identificador único
    public string Nome { get; set; } = string.Empty;        // Nome do produto
    public double PrecoUnit { get; set; }   // Preço unitário
    public string Descricao { get; set; } = string.Empty;        // Descrição (opcional)

    // Construtor vazio necessário para Dapper ou deserialização automática
    public Produto() { }

    // Construtor para uso em código
    public Produto(string sku, string nome, double precoUnit, string desc, int idProduto = 0)
    {
        IdProduto = idProduto;
        Sku = sku;
        Nome = nome;
        PrecoUnit = precoUnit;
        Descricao = desc;
    }
}
