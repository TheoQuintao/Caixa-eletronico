using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;

class Program
{
    public static void Main()
    {
        Console.Clear();
        HashSet<string> usuarios = new HashSet<string>();
        HashSet<int> contas = new HashSet<int>();
        List<int> saldos = new List<int>();
        int saldo = 1001;
        saldos.Add(saldo);
        int conta = 0;
        int valordosaque = 0;
        SigIn(contas,usuarios);
        LogIn(ref conta,contas,usuarios);
        Console.WriteLine(conta);
        Saque(500,conta,saldos,contas);
        Console.WriteLine(saldos[0]);
    }
    public static void SigIn(HashSet<int> contas, HashSet<string> usuarios)
    {
        Random rnd = new Random();
        string cpf;
        while (true)
        {
            Console.Write("Número do CPF: ");
            cpf = Console.ReadLine()?.Trim() ?? "";

            if (ValidarCpf(cpf))
            {
                if (!usuarios.Add(cpf))
                {
                    Console.WriteLine("CPF já cadastrado!!!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
                else
                {
                    int conta;
                    do
                    {
                        conta = rnd.Next(100000, 999999);
                    } while (!contas.Add(conta));

                    Console.WriteLine($"O número da sua conta é: {conta}");
                    break;
                }
            }
            else
                Console.Clear();
        }
    }

    public static bool ValidarCpf(string cpf)
    {
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        cpf = cpf.Replace(".", "").Replace("-", "").Trim();
        if (cpf.Length != 11) return false;

        for (int j = 0; j < 10; j++)
            if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                return false;

        string tempCpf = cpf.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        tempCpf += resto;

        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        tempCpf += resto;

        return cpf.EndsWith(tempCpf.Substring(9));
    }

    public static bool LogIn(ref int conta,HashSet<int> contas, HashSet<string> usuarios)
    {
        Console.Write("CPF: ");
        string cpf = Console.ReadLine().Replace(".", "").Replace("-", "").Trim();

        if (!usuarios.Contains(cpf))
        {
            Console.WriteLine("CPF não cadastrado");
            Thread.Sleep(3000);
            Console.Clear();
            return false;
        }

        
        Console.Write("Conta: ");
        if (int.TryParse(Console.ReadLine(), out conta) && contas.Contains(conta))
            return true;
        else   
            return false;   
    }
    public static void Saque(int valordosaque,int conta,List<int> saldos, HashSet<int> contas)
    {
        foreach(int i in contas)
        {
            int j = 0;
            if(i == conta)
            {
                saldos[j] -= valordosaque;
                break;
            }
            j++;
        }
    }
}
