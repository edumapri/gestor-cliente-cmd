using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GestorClientesCmd
{
    class Program
    {
        // Vai permitir a gravação dos itens do tipo "struct" e "list"
        [System.Serializable]
        // Struct é similar a uma classe
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        // Variável global
        static List<Cliente> clientes = new List<Cliente>();

        // menu
        enum Menu { Listagem = 1, Adicionar = 2, Remover = 3, Sair = 4 }
        
        static void Main(string[] args)
        {
            Carregar();

            bool escolherSair = false;

            while (!escolherSair)
            {
                Console.WriteLine("Sistema de clientes - Bem vindo");
                Console.WriteLine("1-Listagem");
                Console.WriteLine("2-Adicionar");
                Console.WriteLine("3-Remover");
                Console.WriteLine("4-Sair\n");

                // Converter dado inteiro digitado pelo usuário
                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp;

                switch (opcao)
                {
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        escolherSair = true;
                        break;
                }
                // Limpar o console
                Console.Clear();
            }

        }

        // Função void para (salvar) o cadastro do cliente
        static void Adicionar()
        {
            Cliente cliente = new Cliente();// struct client
            Console.WriteLine("Cadastro de cliente:\n");
            Console.WriteLine("Nome do cliente:");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("E-mail do cliente:");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente:");
            cliente.cpf = Console.ReadLine();

            // Adicionar o clientes na lista cliente na lista clientes
            clientes.Add(cliente);
            Salvar();

            Console.WriteLine("O(a) cliente " + cliente.nome + " foi cadastrado(a) com sucesso\n");
            Console.WriteLine("Pressione <enter> para sair");
            Console.ReadLine();
            Console.Clear();
        }

        // Função que vai listar os clientes
        static void Listagem()
        {
            if (clientes.Count > 0) // Se tem pelo menos um cliente
            {
                Console.WriteLine("\nLista de clientes: \n");
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"E-mail: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("==============================\n");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado");
            }
                Console.WriteLine("Pressione <enter> para continuar");
                Console.ReadLine();
            
        }

        // Função que vai remover clientes
        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID do cliente que você quer remover");
            int id = int.Parse(Console.ReadLine());
            if(id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();
            }
            else
            {
                Console.WriteLine("Id digitado é inválido, tente novamente!");
                Console.ReadLine();
            }
        }



        // Função responsável por salvar os clientes
        static void Salvar()
        {
            FileStream stream = new FileStream("clientes.txt", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clientes);

            stream.Close();
        }

        // Função responsável por carregar os clientes
        static void Carregar()
        {
            FileStream stream = new FileStream("clientes.txt", FileMode.OpenOrCreate);
            try
            {
                
                BinaryFormatter encoder = new BinaryFormatter();

                clientes = (List<Cliente>)encoder.Deserialize(stream);

                if(clientes == null)
                {
                    clientes = new List<Cliente>();
                }

                
            }   
            catch(Exception e)
            {
                clientes = new List<Cliente>();
            }

            stream.Close();
        }
    }
}