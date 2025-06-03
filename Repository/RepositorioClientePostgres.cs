using sysvendas2.Interfaces;
using sysvendas2.Models;
using Dapper;
using Npgsql;

namespace sysvendas2.Repository;

public class RepositorioClientePostgres : IRepositorioCliente
{
    private readonly string _connStr;

    public RepositorioClientePostgres(string connStr)
    {
        _connStr = connStr;
    }

    public void CriarTabelas()
    {
        var sql = @"
        CREATE TABLE IF NOT EXISTS Clientes (
            IdCliente SERIAL PRIMARY KEY,
            Nome TEXT NOT NULL,
            Email TEXT NOT NULL,
            Telefone TEXT NOT NULL
        );";
        using var conn = new NpgsqlConnection(_connStr);
        conn.Execute(sql);
    }

    public void Adicionar(Cliente cliente)
    {
        var sql = "INSERT INTO Clientes (Nome, Email, Telefone) VALUES (@Nome, @Email, @Telefone)";
        using var conn = new NpgsqlConnection(_connStr);
        conn.Execute(sql, cliente);
    }

    public List<Cliente> ObterTodos()
    {
        var sql = "SELECT * FROM Clientes ORDER BY Nome";
        using var conn = new NpgsqlConnection(_connStr);
        return conn.Query<Cliente>(sql).ToList();
    }

    public Cliente? ObterClienteId(int idcliente)
    {
        var sql = "SELECT * FROM Clientes WHERE IdCliente = @IdCliente";
        using var conn = new NpgsqlConnection(_connStr);
        return conn.QueryFirstOrDefault<Cliente>(sql, new { IdCliente = idcliente });
    }

    public Cliente? ObterClienteNome(string nome)
    {
        var sql = "SELECT * FROM Clientes WHERE Nome ILIKE @Nome";
        using var conn = new NpgsqlConnection(_connStr);
        return conn.QueryFirstOrDefault<Cliente>(sql, new { Nome = $"%{nome}%" });
    }
}
