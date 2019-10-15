using System;
using System.Threading;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp
{
    public class Conta
    {
        public string Titular { get; set; }
        public string Cpf { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Telefone { get; set; }
        public string Agencia { get; set; }
        public string NumeroConta { get; set; }

        public double Saldo { get; set; }
        public string linha { get; private set; }

        string tempFile = "C:\\Users\\pedro.dantas\\source\\repos\\Projeto_Pai\\ConsoleApp\\tempContas\\Conta.txt";

        string Caminho = "C:\\Users\\pedro.dantas\\source\\repos\\Projeto_Pai\\ConsoleApp\\Contas\\Conta.txt";

        public void Menu()
        {
        inicio:
            Console.WriteLine("=================================================================================\n");
            Console.WriteLine("Olá, o que você deseja fazer? Precione um número | 1 | 2 | 3 | 4 |\n");
            Console.WriteLine("1 - Cadastrar uma nova conta");
            Console.WriteLine("2 - Consultar conta");
            Console.WriteLine("3 - Sair\n");

            ConsoleKeyInfo MenuSelection = Console.ReadKey(true);

            switch (MenuSelection.KeyChar)
            {
                case '1':
                    CriarConta();
                    break;
                case '2':
                    ConsultarConta();
                    break;
                case '3':
                    Environment.Exit(1);
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Erro ao escolhar um opção! Tente novamente!");
                    Thread.Sleep(300);
                    Console.ResetColor();
                    Console.Clear();
                    goto inicio;
            }
        }

        public void CriarConta()
        {
            inicio:
            Console.Clear();
            Console.WriteLine("=================================================================================\n");
            Console.WriteLine("Entre com seus dados: ");
        nome:
            try
            {
                Console.WriteLine("\nNome do titular: ");
                Titular = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                goto nome;
            }

        cpf:
            try
            {
                Console.WriteLine("\nCPF do titular: ");
                Cpf = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                goto cpf;
            }

        bairro:
            try
            {
                Console.WriteLine("\nBairro: ");
                Bairro = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                goto bairro;
            }

        cidade:
            try
            {
                Console.WriteLine("\nCidade: ");
                Cidade = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                goto cidade;
            }

        telefone:
            try
            {
                Console.WriteLine("\nTelefone: \n");
                Telefone = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                goto telefone;
            }

            Console.Clear();

            Console.WriteLine("\nConfirma a criação da conta com as seguintes informações?");
            Console.WriteLine(

                "\nTitular: " + Titular +
                "\nCPF:     " + Cpf +
                "\nCidade:  " + Cidade +
                "\nBairro:  " + Bairro +
                "\nTelefone:" + Telefone +
                "\nSaldo:   " + Saldo);

            Console.WriteLine("\n| 1 - Confirmar | 2 - Voltar |");
            ConsoleKeyInfo MenuSelection = Console.ReadKey(true);

            switch (MenuSelection.KeyChar)
            {
                case '1':
                    Random rnd = new Random();
                    Agencia = Convert.ToString(rnd.Next(1000, 9999));
                    NumeroConta = Convert.ToString(rnd.Next(100000, 999999));

                    if (ContaDataSource.Save(this))
                    {
                        Console.WriteLine("\nConta cadastrada com sucesso!");
                        Console.WriteLine($"\nO seu numero de conta é: {NumeroConta}");
                        Console.WriteLine($"\nO seu numero de agencia é: {Agencia}");
                        Thread.Sleep(5000);
                        Console.Clear();
                        Menu();
                    }
                    else
                    {
                        Console.WriteLine("Erro ao cadastrar a conta! Tente novamente");
                        Menu();
                    }
                    break;
                case '2':
                    Console.Clear();
                    Menu();
                    break;

                default:
                    goto inicio;
            }
        }

        public void EditarConta(Conta conta)
        {
            Console.Clear();

            try
            {
                Conta NovaContaEditada = new Conta();
                NovaContaEditada.Saldo = conta.Saldo;
                NovaContaEditada.Agencia = conta.Agencia;
                NovaContaEditada.NumeroConta = conta.NumeroConta;

                Console.WriteLine("Titular atual: " + conta.Titular);
                Console.WriteLine("Digite um novo Titular: ");
                NovaContaEditada.Titular = Console.ReadLine();

                Console.WriteLine("\nCpf atual: " + conta.Cpf);
                Console.WriteLine("Digite um novo Cpf: ");
                NovaContaEditada.Cpf = Console.ReadLine();

                Console.WriteLine("\nCidade atual: " + conta.Cidade);
                Console.WriteLine("Digite um novo Cidade: ");
                NovaContaEditada.Cidade = Console.ReadLine();

                Console.WriteLine("\nBairro atual: " + conta.Bairro);
                Console.WriteLine("Digite um novo Bairro: ");
                NovaContaEditada.Bairro = Console.ReadLine();

                Console.WriteLine("\nTelefone atual: " + conta.Telefone);
                Console.WriteLine("Digite um novo Telefone: ");
                NovaContaEditada.Telefone = Console.ReadLine();

                ContaDataSource.AtualizarConta(NovaContaEditada);

                Console.WriteLine("\nConta aleterada com sucesso!");
                Console.WriteLine("\nTitular: " + NovaContaEditada.Titular + "\nCpf: " + NovaContaEditada.Cpf + "\nCidade: " + NovaContaEditada.Cidade + "\nBairro: " + NovaContaEditada.Bairro 
                    + "\nTelefone: " + NovaContaEditada.Telefone + "\nAgência: " + NovaContaEditada.Agencia + "\nConta: " + NovaContaEditada.NumeroConta + "\nSaldo: " + NovaContaEditada.Saldo);

                OperacaoConta(NovaContaEditada);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Não foi possivel alterar a conta: " + ex.Message);
                OperacaoConta(conta);
            }
        }
        
        private void Sacar(double OperacaoSaque, Conta conta)
        {
            Console.Clear();
            Conta ValorOperacaoSaque = new Conta();
            ValorOperacaoSaque.Titular = conta.Titular;
            ValorOperacaoSaque.Cpf = conta.Cpf;
            ValorOperacaoSaque.Cidade = conta.Cidade;
            ValorOperacaoSaque.Bairro = conta.Bairro;
            ValorOperacaoSaque.Telefone = conta.Telefone;
            ValorOperacaoSaque.NumeroConta = conta.NumeroConta;
            ValorOperacaoSaque.Agencia = conta.Agencia;
            ValorOperacaoSaque.Saldo = conta.Saldo;

            if (ValorOperacaoSaque.Saldo >= OperacaoSaque)
            {
                ValorOperacaoSaque.Saldo -= OperacaoSaque;
            }
            else {
                Console.WriteLine("Saldo insuficiente para o saque!");
                Console.Clear();
                OperacaoConta(conta);
            }
         
            string menssagem = "Sacar: R$ " + OperacaoSaque + ", com o saldo atual de: R$ " + ValorOperacaoSaque.Saldo;

            ContaDataSource.SaveExtrato(ValorOperacaoSaque, menssagem);

            ContaDataSource.AtualizarConta(ValorOperacaoSaque);

            Console.WriteLine("\nSaque feito com sucesso!");
            Console.WriteLine("\nTitular: " + ValorOperacaoSaque.Titular + "\nCpf: " + ValorOperacaoSaque.Cpf + "\nCidade: " + ValorOperacaoSaque.Cidade + "\nBairro: " + ValorOperacaoSaque.Bairro
                + "\nTelefone: " + ValorOperacaoSaque.Telefone + "\nAgência: " + ValorOperacaoSaque.Agencia + "\nConta: " + ValorOperacaoSaque.NumeroConta + "\nSaldo: " + ValorOperacaoSaque.Saldo);

            OperacaoConta(ValorOperacaoSaque);
        }
        private void Depositar(double ValorOperacaoDeposita, Conta conta)
        {
            Console.Clear();
            Conta NovoDepositoEditado = new Conta();
            NovoDepositoEditado.Titular = conta.Titular;
            NovoDepositoEditado.Cpf = conta.Cpf;
            NovoDepositoEditado.Cidade = conta.Cidade;
            NovoDepositoEditado.Bairro = conta.Bairro;
            NovoDepositoEditado.Telefone = conta.Telefone;
            NovoDepositoEditado.NumeroConta = conta.NumeroConta;
            NovoDepositoEditado.Agencia = conta.Agencia;
            NovoDepositoEditado.Saldo = conta.Saldo;

            NovoDepositoEditado.Saldo += ValorOperacaoDeposita;

            string menssagem = "Deposito: R$ " + ValorOperacaoDeposita + ", com o saldo atual de: R$ " + NovoDepositoEditado.Saldo;

            ContaDataSource.SaveExtrato(NovoDepositoEditado, menssagem);

            ContaDataSource.AtualizarConta(NovoDepositoEditado);

            Console.WriteLine("\nDeposito feito com sucesso!");
            Console.WriteLine("\nTitular: " + NovoDepositoEditado.Titular + "\nCpf: " + NovoDepositoEditado.Cpf + "\nCidade: " + NovoDepositoEditado.Cidade + "\nBairro: " + NovoDepositoEditado.Bairro
                + "\nTelefone: " + NovoDepositoEditado.Telefone + "\nAgência: " + NovoDepositoEditado.Agencia + "\nConta: " + NovoDepositoEditado.NumeroConta + "\nSaldo: " + NovoDepositoEditado.Saldo);

            OperacaoConta(NovoDepositoEditado);
        }
        public void OperacaoConta(Conta conta)
        {
        inicio:
            Console.WriteLine("==================================================================================================\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Olá, que tipo de operação você deseja fazer?\n" +
            "\n\t | 1 - Voltar | 2 - Depositar | 3 - Sacar | 4 - Consultar extrato | 5 - Editar conta | 6 - Deletar Conta | \n");
            Console.ResetColor();

            ConsoleKeyInfo MenuSelection = Console.ReadKey(true);
            switch (MenuSelection.KeyChar)
            {
                case '1':
                    Console.Clear();
                    Menu();
                    break;

                case '2':
                    Console.WriteLine("\nDigite o valor que você deseja depositar: ");
                    double ValorOperacaoDeposita = Convert.ToDouble(Console.ReadLine());
                    Depositar(ValorOperacaoDeposita, conta);
                    break;
                case '3':
                    Console.WriteLine("\nDigite o valor que você deseja sacar: ");
                    double ValorOperacaoSaque = Convert.ToDouble(Console.ReadLine());
                    Sacar(ValorOperacaoSaque, conta);
                    break;
                case '4':
                    string CaminhoExtrato = "C:\\Users\\pedro.dantas\\source\\repos\\Projeto_Pai\\ConsoleApp\\ExtratoContas\\";
                    string NomeArquivo = $"{conta.Agencia}{conta.NumeroConta}";
                    StreamReader Extrato = File.OpenText(CaminhoExtrato + NomeArquivo);

                    Console.WriteLine("Extrato: " + " Agencia: " + conta.Agencia + ", Numero da conta: " + conta.NumeroConta);

                    while (Extrato.EndOfStream != true)
                    {
                        string linha = Extrato.ReadLine();
                        Console.WriteLine(linha);
                        Thread.Sleep(50);
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nDigite qualquer tecla para continuar!");
                    Console.ReadKey(true);
                    Console.ResetColor();
                    Console.Clear();
                    Menu();
                    break;
                case '5':
                    EditarConta(conta);
                    break;
                case '6':
                    DeletarConta(conta);
                    break;
                default:
                    goto inicio;
            }
        }

        private void DeletarConta(Conta conta)
        {
            StreamReader LerConta = File.OpenText(Caminho);

            List<string> LinhaContaParaManter = new List<string>() {};

            while (LerConta.EndOfStream != true)
            {
                string linha = LerConta.ReadLine();

                string[] DadosConta = linha.Split(',');
                Conta LinhaParaManter = new Conta();
                LinhaParaManter.Titular = DadosConta[0].Split('=')[1];
                LinhaParaManter.Cpf = DadosConta[1].Split('=')[1];
                LinhaParaManter.Cidade = DadosConta[2].Split('=')[1];
                LinhaParaManter.Bairro = DadosConta[3].Split('=')[1];
                LinhaParaManter.Telefone = DadosConta[4].Split('=')[1];
                LinhaParaManter.Saldo = Convert.ToDouble(DadosConta[5].Split('=')[1]);
                LinhaParaManter.NumeroConta = DadosConta[6].Split('=')[1];
                LinhaParaManter.Agencia = DadosConta[7].Split('=')[1];

                if (LinhaParaManter.Agencia != conta.Agencia && LinhaParaManter.NumeroConta != conta.NumeroConta)
                {
                    LinhaContaParaManter.Add(linha);
                }
        }
        LerConta.Close();
        File.WriteAllLines(tempFile, LinhaContaParaManter);
        File.Delete(Caminho);
        File.Move(tempFile, Caminho);
        Console.WriteLine("Conta deletada com sucesso!");
        Console.Clear();
        Menu();
        }

        public void ConsultarConta()
        {
            Console.Clear();
            Console.WriteLine("==================================================================================================\n");
            Console.WriteLine("Digite o numero da sua Agência: ");
            string InputAgencia = Console.ReadLine();
            Console.WriteLine("Digite o numero da sua Conta: ");
            string InputNumeroConta = Console.ReadLine();

            Conta conta = ContaDataSource.GetByAgenciaEConta(InputAgencia, InputNumeroConta);

            if (conta == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nConta não cadastrada, tente novamente!");
                Thread.Sleep(3000);
                Console.ResetColor();
                Console.Clear();
                Menu();
            }
            Console.WriteLine("\nTitular: " + conta.Titular + "\nCpf: " + conta.Cpf + "\nCidade: " + conta.Cidade + "\nBairro: " + conta.Bairro + "\nTelefone: "
                + conta.Telefone + "\nAgência: " + conta.Agencia + "\nConta: " + conta.NumeroConta + "\nSaldo: " + conta.Saldo);

            OperacaoConta(conta);
        }
    }

    public class Programa
    {
        public static void Main()
        {
            Console.CursorVisible = false;
            Conta conta = new Conta();

            if (File.Exists("C:\\Users\\pedro.dantas\\source\\repos\\Projeto_Pai\\ConsoleApp\\Contas\\Conta.txt"))
                conta.ConsultarConta();
            else
                conta.CriarConta();
        }
    }
}