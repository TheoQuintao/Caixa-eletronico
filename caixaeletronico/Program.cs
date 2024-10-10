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
        List<float> saldos = new List<float>();
        
        int conta = 0;

        float valordosaque = 0,
            valordodeposito = 0;
        
        while(true)
        {
            Console.WriteLine("(1)Sou cliente\n(2)Desejo criar uma conta\n(3)Sair\n");
            ConsoleKeyInfo loginmenu = Console.ReadKey();
            switch(loginmenu.KeyChar)
            {
                case '1':
                    if(LogIn(ref conta,contas,usuarios,saldos)==true)
                    {
                        Console.Clear();
                        Menu(valordodeposito,valordosaque,ref conta,saldos,contas,usuarios);
                    }
                break;
                case '2':
                    Console.Clear();
                    SigIn(contas,usuarios,saldos);
                break;
                case '3':
                return;
            } 
        }
        
    }
    public static void SigIn(HashSet<int> contas, HashSet<string> usuarios,List<float> saldos)
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
                    float saldo;
                    int conta;
                    do
                    {
                        conta = rnd.Next(100000, 999999);
                    } while (!contas.Add(conta));
                    saldo = 0;
                    saldos.Add(saldo);

                    Console.WriteLine($"O número da sua conta é: {conta}\n");
                    Thread.Sleep(5000);
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
    public static bool LogIn(ref int conta,HashSet<int> contas, HashSet<string> usuarios,List<float> saldos)
    {
        Console.Write("CPF: ");
        string cpf = Console.ReadLine().Replace(".", "").Replace("-", "").Trim();

        if (!usuarios.Contains(cpf))
        {
            Console.WriteLine("CPF não cadastrado");
            Thread.Sleep(3000);
            Console.Clear();
            Console.WriteLine("Fazer registro?\n\n(1)Sim\n(2)Não\n");
            ConsoleKeyInfo fazerregistro = Console.ReadKey();
            switch(fazerregistro.KeyChar)
            {
                case '1':
                    Console.Clear();
                    SigIn(contas,usuarios,saldos);
                break;
                case '2':
                Console.Clear();
                return false;
            }            
        }

        
        Console.Write("Conta: ");
        if (int.TryParse(Console.ReadLine(), out conta) && contas.Contains(conta))
            return true;
        else   
            return false;   
    }
    public static void Saque(float valordosaque,ref int conta,List<float> saldos, HashSet<int> contas)
    {
        int j = 0;
        foreach(int i in contas)
        {
            j++;
            if(i == conta)
            {
                saldos[j-1] -= valordosaque;
                break;
            }
        }
    }
    public static void Deposito(float valordodeposito,ref int conta,List<float> saldos, HashSet<int> contas)
    {   
        int j = 0;
        foreach(int i in contas)
        {
            j++;
            if(i == conta)
            {
                saldos[j-1] += valordodeposito;
                break;
            }
        }
    }
    public static void Menu(float valordodeposito, float valordosaque,ref int conta,List<float> saldos, HashSet<int> contas,HashSet<string> usuarios)
    {
        while(true)
        {
            Console.WriteLine($"Conta: {conta}\n");
            int j = 0;
            foreach(int i in contas)
            {
                j++;
                if(i == conta)
                {
                    Console.WriteLine($"Saldo: R${saldos[j-1]:F2}\n");
                }
            }
            Console.WriteLine("(1)Deposito\n(2)Saque\n(3)Transferencia\n(4)Sair");
            ConsoleKeyInfo keymenu = Console.ReadKey();
            switch(keymenu.KeyChar)
            {
                case '1':
                    Console.Clear();
                    vd:
                    Console.Write("Valor do deposito: ");
                    if(!float.TryParse(Console.ReadLine(),out valordodeposito))
                        goto vd;
                    Console.Clear();
                    Deposito(valordodeposito,ref conta,saldos,contas);
                break;
                case '2':
                    Console.Clear();
                    vs:
                    Console.Write("Valor do saque: ");
                    if(!float.TryParse(Console.ReadLine(),out valordosaque))
                        goto vs;
                    Console.Clear();
                    Saque(valordosaque,ref conta,saldos,contas);
                break;
                case '3':

                break;
                case '4':
                Console.Clear();
                return;
            }
        }
    }
}
