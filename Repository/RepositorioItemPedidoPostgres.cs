using System.Runtime.CompilerServices;
using sysvendas2.Interfaces;
using Dapper;
using Npgsql;
using sysvendas2.Models;

namespace sysvendas2.Repository;

public class RepositorioItemPedidoPostgres : IRepositorioItemPedido
{
    private readonly string _connStr;

    public RepositorioItemPedidoPostgres(string connStr)
    {
        _connStr = connStr;
    }

    public void CriarTabela()
    {
        var sql = @"
        CREATE TABLE IF NOT EXISTS ItensPedido (
            IdItemPedido SERIAL PRIMARY KEY,
            IdPedido INT NOT NULL REFERENCES Pedidos(IdPedido) ON DELETE CASCADE,
            IdProduto INT NOT NULL REFERENCES Produtos(IdProduto),
            Quantidade INT NOT NULL,
            Desconto INT NOT NULL,
            Preco DECIMAL(10,2) NOT NULL,
            SubTotal DECIMAL(10,2) NOT NULL
        );";

        using var conn = new NpgsqlConnection(_connStr);
        conn.Execute(sql);
    }

    public void Adicionar(ItemPedido item)
    {
        var sql = @"
        INSERT INTO ItensPedido (IdPedido, IdProduto, Quantidade, Desconto, Preco, SubTotal)
        VALUES (@IdPedido, @IdProduto, @Quantidade, @Desconto, @Preco, @SubTotal);";

        using var conn = new NpgsqlConnection(_connStr);
        conn.Execute(sql, item);
       

    }

  
}
