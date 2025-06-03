namespace sysvendas2.Telas;
using System.Text;
using sysvendas2.Models;

static class TelaPrincipal
{
    public static List<Opcao> opcoes;
    static TelaPrincipal()
    {
        opcoes = new List<Opcao>
        {
            new Opcao(1, "😺 Cadastrar cliente"),
            new Opcao(2, "📖 Listar clientes"),
            new Opcao(3, "📦 Cadastrar produto"),
            new Opcao(4, "📋 Listar produtos"),
            new Opcao(5, "📋 Buscar Cliente"),
            new Opcao(6, "📋 Adicionar Pedido"),
            new Opcao(7, "🚪 Sair")
        };
        
        
    }
    public static void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            ExibeTitulo();
            foreach (var opt in opcoes)
            {
                Console.WriteLine($"{opt.Id} - {opt.Descricao}");
            }
        
            Console.WriteLine("\nDigite a opção desejada:");

            if (int.TryParse(Console.ReadLine(), out int opcaoSelecionada))
            {
                if (opcaoSelecionada == 7)
                {
                    Console.WriteLine("\nSaindo...");
                    break;
                } else if (opcaoSelecionada == 1)
                {
                    Console.WriteLine("\nCadastrando clientes");
                    TelaCadastroCliente.Show();
                }
                else if (opcaoSelecionada == 2)
                {
                    Console.WriteLine("\nListando clientes");
                    TelaListaClientes.Show();
                }
                else if (opcaoSelecionada == 3)
                {
                    Console.WriteLine("\nCadastrando produtos");
                    TelaCadastroProduto.Show();
                }
                else if (opcaoSelecionada == 4)
                {
                    Console.WriteLine("\nListando produtos");
                    TelaListaProdutos.Show();
                }
                else if (opcaoSelecionada == 5)
                {
                    Console.WriteLine("\nBuscando clientes");
                    TelaBuscaCliente.Show();
                }
                else if (opcaoSelecionada == 6)
                {
                    Console.WriteLine("\nAdicioando pedido");
                    TelaCadastroPedido.Show();
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida. Digite um número.");
            }
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
      
    }
    private static void ExibeTitulo()
    {
        Console.WriteLine("=======================================");
        Console.WriteLine("=========== 🔥 SYSVENDAS 2 🔥==========");
        Console.WriteLine("=======================================");
    }
}