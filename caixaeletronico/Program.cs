using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

class Program
{
    public static void Sigin(HashSet<int> Contas, HashSet<string> Usuarios, string cpf)
    {
        Random rnd = new Random();
    parauser:
        Console.Write("Número do CPF: ");
        cpf = Console.ReadLine() ?? "";
        if (Validarcpf(cpf) == false)
        {
            Console.Clear();
            goto parauser;
        }
        else
        {
            if (!Usuarios.Add(cpf))
            {
                Console.WriteLine("CPF já cadastrado!!!");
                Thread.Sleep(3000);
                Console.Clear();
                return;
            }
            else
            {
            addconta:
                int conta = rnd.Next(100000, 999999);
                if (!Contas.Add(conta))
                {
                    goto addconta;
                }
                else
                {
                    Console.WriteLine($"O número da sua conta e: {conta}");
                }

            }
        }
    }
    public static bool Validarcpf(string cpf)
    {
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        cpf = cpf.Trim().Replace(".", "").Replace("-", "");
        if (cpf.Length != 11)
            return false;

        for (int j = 0; j < 10; j++)
            if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                return false;

        string tempCpf = cpf.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        string digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return cpf.EndsWith(digito);
    }
    public static void Login(int logado, HashSet<int> Contas, HashSet<string> Usuarios)
    {
        List<string> usurioslogin = Usuarios.ToList();
        List<int> contaslogin = Contas.ToList();

        Console.Write("CPF: ");
        string cpflogin = Console.ReadLine().Replace(".", "").Replace("-", "");
        if (Usuarios.Add(cpflogin))
        {
            Console.WriteLine("CPF não cadastrado");
            Usuarios.Remove(cpflogin);
            Thread.Sleep(3000);
            Console.Clear();
            return;
        }
        else
        {
        cl:
            Console.Write("Conta: ");
            if (!int.TryParse(Console.ReadLine(), out int conta))
                goto cl;
            else
            {
                foreach (string valido in usurioslogin)
                {
                    int i = 0;
                    if (valido == cpflogin)
                    {
                        if (conta == contaslogin[i])
                        {
                            Console.WriteLine("Logado");
                            logado = 1;
                        }
                    }
                    else
                        i++;
                }
            }
        }

    }
    public static void Main()
    {
        Console.WriteLine("Estou aqui1");
        Console.Clear();
        HashSet<string> Usuarios = new HashSet<string>();
        HashSet<int> Contas = new HashSet<int>();
        Console.WriteLine("Estou aqui2");
        string cpf = "";
        int logado = 0;
        Sigin(Contas, Usuarios, cpf);
        Login(logado, Contas, Usuarios);
        Console.WriteLine("Estou aqui3");
        if (logado == 1)
        {
            Console.WriteLine("Você está logado");
        }
        else if (logado == 0)
            Console.WriteLine("estou aqui4");
    }
}