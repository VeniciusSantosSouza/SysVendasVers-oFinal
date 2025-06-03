using System.Text;
using sysvendas2.Context;
using sysvendas2.Models;

namespace sysvendas2.Telas;

static class TelaCadastroCliente
{
    //public static List<Cliente> clientes;
    
    static TelaCadastroCliente()
    {
        //clientes = new List<Cliente>(); 
    }

    public static void Show()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        ExibeTitulo();
        
        string nomeCliente = "";
        string emailCliente = "";
        string telefoneCliente = "";
        
        Console.WriteLine("\nDigite o ID do cliente:");
        int.TryParse(Console.ReadLine(), out int _idCliente);
        
        Console.WriteLine("\nDigite o nome do cliente:");
        nomeCliente = Console.ReadLine();
        
        Console.WriteLine("\nDigite o email do cliente:");
        emailCliente = Console.ReadLine();
        
        Console.WriteLine("\nDigite o telefone do cliente:");
        telefoneCliente = Console.ReadLine();

        Cliente cliente = new Cliente(_idCliente, nomeCliente, emailCliente, telefoneCliente);
        //clientes.Add(cliente);
        DBContext.RepositorioClientes.Adicionar(cliente);
        
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
        TelaPrincipal.Show();
    }

    private static void ExibeTitulo()
    {
        Console.WriteLine("=======================================");
        Console.WriteLine("====== 🧑‍🦳 CADASTRO DE CLIENTES 🧑‍🦳======");
        Console.WriteLine("=======================================");
    }
}

/*
        cliente = new List<Cliente>
        {
            new Opcao(1," ❤️ Cadastrar cliente"),
            new Opcao(2, "Listar clientes"),
            new Opcao(3, "Sair")
        };
*/