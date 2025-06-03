using System.Text;
using sysvendas2.Context;

namespace sysvendas2.Telas;

static class TelaListaClientes
{
    
    static TelaListaClientes()
    {
        
    }

    public static void Show()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        ExibeTitulo();
        if (DBContext.RepositorioClientes != null)
            foreach (var cliente in DBContext.RepositorioClientes.ObterTodos())
            {
                Console.WriteLine($"{cliente.IdCliente}: {cliente.Nome} - {cliente.Email}");
            }

        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
        TelaPrincipal.Show();
    }

    private static void ExibeTitulo()
    {
        Console.WriteLine("=======================================");
        Console.WriteLine("======= 🧑‍🦳 LISTA DE CLIENTES 🧑‍🦳========");
        Console.WriteLine("=======================================");
    }
}