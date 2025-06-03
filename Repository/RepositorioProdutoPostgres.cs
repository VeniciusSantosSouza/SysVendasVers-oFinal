using Dapper;
using Npgsql;
using sysvendas2.Interfaces;
using sysvendas2.Models;

namespace sysvendas2.Repository;

public class RepositorioProdutoPostgres : IRepositorioProduto
{
    private readonly string _connStr;

    public RepositorioProdutoPostgres(string connStr)
    {
        _connStr = connStr;
    }

    public void CriarTabelas()
    {
        var sql = @"
        CREATE TABLE IF NOT EXISTS Produtos (
            IdProduto SERIAL PRIMARY KEY,
            Nome TEXT NOT NULL,
            Sku TEXT NOT NULL UNIQUE,
            PrecoUnit DECIMAL(10,2) NOT NULL,
            Descricao TEXT
        );
        ";

        using var conn = new NpgsqlConnection(_connStr);
        conn.Execute(sql);
    }

    public void Adicionar(Produto produto)
    {
        var sql = @"
        INSERT INTO Produtos (Nome, Sku, PrecoUnit, Descricao)
        VALUES (@Nome, @Sku, @PrecoUnit, @Descricao);";

        using var conn = new NpgsqlConnection(_connStr);
        conn.Execute(sql, produto);
    }

    public List<Produto> ObterTodos()
    {
        var sql = "SELECT * FROM Produtos ORDER BY Nome";

        using var conn = new NpgsqlConnection(_connStr);
        return conn.Query<Produto>(sql).ToList();
    }

    public Produto? ObterProduto(string sku)
    {
        var sql = "SELECT * FROM Produtos WHERE Sku = @Sku";
        using var conn = new NpgsqlConnection(_connStr);
        return conn.QueryFirstOrDefault<Produto>(sql, new { Sku = sku });
    }
}
