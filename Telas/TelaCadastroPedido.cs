using System.Text;
using System.Threading.Tasks;
using sysvendas2.Context;
using sysvendas2.Models;

namespace sysvendas2.Telas;

static class TelaCadastroPedido
{
    public static void Show()
    {
        Console.Clear(); // Limpa a tela do console
        Console.OutputEncoding = Encoding.UTF8;
        ExibeTitulo();

        Console.WriteLine("\nDigite o ID do pedido:");
        int.TryParse(Console.ReadLine(), out int idPedido);

        Console.WriteLine("\nDigite o ID do cliente:");
        int.TryParse(Console.ReadLine(), out int idCliente); 

        Cliente cliente = DBContext.RepositorioClientes.ObterClienteId(idCliente);
        if (cliente == null)
        {
            Console.WriteLine($"\nCliente com ID {idCliente} não encontrado!"); 
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal...");
            Console.ReadKey();
            TelaPrincipal.Show();
            return;
        }
        DateTime dataPedido = DateTime.Now; 
        string statusPedido = "Em Aberto";

        //Cadastra e retorna o PEDIDO do banco de dados
        Pedido pedidoDb =   DBContext.RepositorioPedidos.Adicionar(new Pedido(idPedido, dataPedido, cliente, statusPedido));
        pedidoDb.Items = new List<ItemPedido>();
        
        bool adicionarMaisItens = true;
        while (adicionarMaisItens) 
        {
            AdicionarItemAoPedido(pedidoDb); 
            Console.WriteLine("\nDeseja adicionar mais itens ao pedido? (s/n): ");
            adicionarMaisItens = Console.ReadLine().ToLower() == "s"; 
        }

       
        pedidoDb.Total = pedidoDb.Items.Sum(item => item.SubTotal);
       

        Console.WriteLine("\nPedido cadastrado com sucesso!");
        ExibeResumoPedido(pedidoDb);

        Console.WriteLine("\nPressione quealquer tecla para voltar ao menu principal...");
        Console.ReadKey();
        TelaPrincipal.Show();
    }

    private static void ExibeTitulo()
    {
        Console.WriteLine("=======================================");
        Console.WriteLine("========= 🛒 CADASTRO DE PEDIDO 🛒 =========");
        Console.WriteLine("=======================================");
    }

    private static void AdicionarItemAoPedido(Pedido pedido)
    {
        Console.WriteLine("\n--- Adicionar Item ao Pedido ---");

        Console.WriteLine("Digite o SKU do produto: ");
        string sku = Console.ReadLine();
        Produto produto = DBContext.RepositorioProdutos.ObterProduto(sku);
        if (produto == null)
        {
            Console.WriteLine($"Produto com SKU {sku} não encontrado!");
        }
        else
        {
            Console.WriteLine("Digite a quantidade: ");
            int.TryParse(Console.ReadLine(), out int quantidade); 
            Console.WriteLine("Digite o desconto (%): ");
            int.TryParse(Console.ReadLine(), out int desconto);
            ItemPedido item = new ItemPedido
            {
                Produto = produto,
                IdProduto = produto.IdProduto,
                IdPedido  = pedido.IdPedido,
                Quantidade = quantidade,
                Desconto = desconto,
                Preco = produto.PrecoUnit,
                SubTotal = (produto.PrecoUnit * quantidade) * (1 - desconto / 100.0)
            };
            pedido.Items.Add(item);
            DBContext.RepositorioItemPedidos.Adicionar(item);  
        }
    }

    private static void ExibeResumoPedido(Pedido pedido)
    {
        Console.WriteLine("\n--- Resumo do Pedido ---");
        Console.WriteLine($"ID: {pedido.IdPedido}");
        Console.WriteLine($"Data: {pedido.Data}");
        Console.WriteLine($"Cliente: {pedido.Cliente.Nome} (ID: {pedido.Cliente.IdCliente})");
        Console.WriteLine($"Status: {pedido.Status}");

        Console.WriteLine("\n--- Itens do Pedido ---");
        foreach (var item in pedido.Items) // Mostra cada item do pedido
        {
            Console.WriteLine($"- {item.Produto.Nome} (SKU: {item.Produto.Sku})");
            Console.WriteLine($"  Quantidade: {item.Quantidade}, Preço Unitário: {item.Preco}, Desconto: {item.Desconto}%, Subtotal: {item.SubTotal}");
        }

        Console.WriteLine($"\nTotal do Pedido: {pedido.Total}"); // Mostra o total do pedido
    }
}