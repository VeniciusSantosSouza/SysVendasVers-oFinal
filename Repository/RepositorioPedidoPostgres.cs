using Dapper;
using Npgsql;
using sysvendas2.Interfaces;
using sysvendas2.Models;

namespace sysvendas2.Repository;

public class RepositorioPedidoPostgres : IRepositorioPedido
{
    private readonly string _connStr;
    private readonly IRepositorioCliente _repositorioCliente;

    public RepositorioPedidoPostgres(string connStr, IRepositorioCliente repositorioCliente)
    {
        _connStr = connStr;
        _repositorioCliente = repositorioCliente;
    }

    public void CriarTabelas()
    {
        var sql = @"
        CREATE TABLE IF NOT EXISTS Pedidos (
            IdPedido SERIAL PRIMARY KEY,
            IdCliente INTEGER NOT NULL,
            DataPedido TIMESTAMP NOT NULL,
            ValorTotal DECIMAL(10,2) NOT NULL,
            Status TEXT NOT NULL, -- NOVO: adicionando coluna Status
            FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente)
        );";
        
        using var conn = new NpgsqlConnection(_connStr);
        conn.Execute(sql);
    }

    public Pedido Adicionar(Pedido pedido)
    {
        var sql = @"
        INSERT INTO Pedidos (IdCliente, DataPedido, ValorTotal, Status)
        VALUES (@IdCliente, @DataPedido, @ValorTotal, @Status)
        RETURNING *"; // NOVO: incluindo retorno de Status

        using var conn = new NpgsqlConnection(_connStr);
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("IdCliente", pedido.IdCliente);
        cmd.Parameters.AddWithValue("DataPedido", pedido.Data);
        cmd.Parameters.AddWithValue("ValorTotal", pedido.Total);
        cmd.Parameters.AddWithValue("Status", pedido.Status); // NOVO: status incluído

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            int idPedido = reader.GetInt32(0);
            int idCliente = reader.GetInt32(1);
            DateTime dataPedido = reader.GetDateTime(2);
            decimal valorTotal = reader.GetDecimal(3);
            string status = reader.GetString(4); // NOVO: lendo status

            var cliente = _repositorioCliente.ObterClienteId(idCliente);
            if (cliente == null) return null;

            var novoPedido = new Pedido(idPedido, dataPedido, cliente, status)
            {
                Total = (double)valorTotal
            };

            return novoPedido;
        }

        return null;
    }

    public List<Pedido> ObterTodos()
    {
        var sql = "SELECT * FROM Pedidos ORDER BY DataPedido DESC";

        using var conn = new NpgsqlConnection(_connStr);
        var pedidosRaw = conn.Query(sql).ToList();

        var pedidos = new List<Pedido>();

        foreach (var row in pedidosRaw)
        {
            var cliente = _repositorioCliente.ObterClienteId((int)row.idcliente);
            if (cliente == null) continue;

            var pedido = new Pedido(
                (int)row.idpedido,
                (DateTime)row.datapedido,
                cliente,
                (string)row.status // NOVO: status vindo do banco
            )
            {
                Total = (double)row.valortotal
            };

            pedidos.Add(pedido);
        }

        return pedidos;
    }
}
