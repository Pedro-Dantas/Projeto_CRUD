using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp
{
    public static class ContaDataSource
    {
        static string Caminho = "C:\\Users\\pedro.dantas\\source\\repos\\Projeto_Pai\\ConsoleApp\\Contas\\Conta.txt";
        static string tempFile = "C:\\Users\\pedro.dantas\\source\\repos\\Projeto_Pai\\ConsoleApp\\tempContas\\Conta.txt";
        static string CaminhoExtrato = "C:\\Users\\pedro.dantas\\source\\repos\\Projeto_Pai\\ConsoleApp\\ExtratoContas\\";

        public static bool Save(Conta conta)
        {
            try
            {
                StreamWriter SalvarConta = File.AppendText(Caminho);
                SalvarConta.WriteLine($"Titular={conta.Titular},CPF={conta.Cpf},Cidade={conta.Cidade},Bairro={conta.Bairro},Telefone={conta.Telefone},Saldo={conta.Saldo},Conta={conta.NumeroConta},Agencia={conta.Agencia}");
                SalvarConta.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possivel criar a conta no momento!" + ex);
                return false;
            }
        }

        public static void SaveExtrato(Conta conta, string menssagem)
        {
            string NomeArquivo = $"{conta.Agencia}{conta.NumeroConta}";
            FileInfo umArquivo = new FileInfo(CaminhoExtrato + NomeArquivo);
 
            try
            {
                DateTime ApenasData = DateTime.Today;

                StreamWriter AtualizarExtrato = umArquivo.AppendText();
                {
                    AtualizarExtrato.WriteLine($"{ApenasData.ToString("d")},{menssagem}");
                    AtualizarExtrato.Close();
                }
            }
            catch
            {
                Console.WriteLine("Não foi possivel salvar o extrato!");
            }
            
        }

        public static void LerExtrato(Conta conta)
        {
            string NomeArquivo = $"{conta.Agencia}{conta.NumeroConta}";
            StreamReader Extrato = File.OpenText(CaminhoExtrato + NomeArquivo);

            Console.WriteLine("Extrato: " + " Agencia: " + conta.Agencia + ", Numero da conta: " + conta.NumeroConta);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nEntre com o primeiro intervalos de data (de)");
            Console.ResetColor();

            Console.WriteLine("\nEntre com o dia: ");
            int DoDia = int.Parse(Console.ReadLine());

            Console.WriteLine("\nEntre com o mês: ");
            int DoMes = int.Parse(Console.ReadLine());

            Console.WriteLine("\nEntre com o ano: ");
            int DoAno = int.Parse(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nEntre com o segundo intervalos de data (até)");
            Console.ResetColor();

            Console.WriteLine("\nEntre com o dia: ");
            int AteDia = int.Parse(Console.ReadLine());

            Console.WriteLine("\nEntre com o mês: ");
            int AteMes = int.Parse(Console.ReadLine());

            Console.WriteLine("\nEntre com o ano: ");
            int AteAno = int.Parse(Console.ReadLine());

            string PrimeiraData = $"{DoDia}/{DoMes}/{DoAno}";

            string SegundaData = $"{DoDia}/{DoMes}/{DoAno}";

            List<string> ExtratoAMostrar = new List<string>();

            while (Extrato.EndOfStream != true)
            {
                string linha = Console.ReadLine();
                string[] DadosExtrato = linha.Split(',');
                string DataExtrato = DadosExtrato[0];

                if (true)
                {

                }
                //else
                    //ExtratoAMostrar.Add(DataExtrato);
            }
            Extrato.Close();
        }

        public static Conta GetByAgenciaEConta(string InputAgencia, string InputNumeroConta)
            {
            StreamReader LerConta = File.OpenText(Caminho);

            while (LerConta.EndOfStream != true)
            {
                string linha = LerConta.ReadLine();
                
                string[] DadosConta = linha.Split(',');
                Conta conta = new Conta()
                {
                    Cpf = DadosConta[1].Split('=')[1],
                    Titular = DadosConta[0].Split('=')[1],
                    Cidade = DadosConta[2].Split('=')[1],
                    Bairro = DadosConta[3].Split('=')[1],
                    Telefone = DadosConta[4].Split('=')[1],
                    Saldo = Convert.ToDouble(DadosConta[5].Split('=')[1]),
                    NumeroConta = DadosConta[6].Split('=')[1],
                    Agencia = DadosConta[7].Split('=')[1]
                };

                if (conta.Agencia == InputAgencia && conta.NumeroConta == InputNumeroConta)
                {
                    LerConta.Close();
                    return conta;
                }
            }
            LerConta.Close();
            return null;
        }

        public static void AtualizarConta(Conta NovaContaAtualizada)
        {
            StreamReader LerConta = File.OpenText(Caminho);

            List<string> ContasAManter = new List<string>();

            while (LerConta.EndOfStream != true)
            {
                string linha = LerConta.ReadLine();

                string[] DadosConta = linha.Split(',');
                Conta ContaAntiga = new Conta
                {
                    Titular = DadosConta[0].Split('=')[1],
                    Cpf = DadosConta[1].Split('=')[1],
                    Cidade = DadosConta[2].Split('=')[1],
                    Bairro = DadosConta[3].Split('=')[1],
                    Telefone = DadosConta[4].Split('=')[1],
                    Saldo = Convert.ToDouble(DadosConta[5].Split('=')[1]),
                    NumeroConta = DadosConta[6].Split('=')[1],
                    Agencia = DadosConta[7].Split('=')[1]
                };

                if ((ContaAntiga.Agencia == NovaContaAtualizada.Agencia) && (ContaAntiga.NumeroConta == NovaContaAtualizada.NumeroConta))
                {
                    ContasAManter.Add($"Titular={NovaContaAtualizada.Titular},CPF={NovaContaAtualizada.Cpf},Cidade={NovaContaAtualizada.Cidade},Bairro={NovaContaAtualizada.Bairro},Telefone={NovaContaAtualizada.Telefone},Saldo={NovaContaAtualizada.Saldo},Conta={NovaContaAtualizada.NumeroConta},Agencia={NovaContaAtualizada.Agencia}");
                }
                else
                    ContasAManter.Add(linha);
            }
            LerConta.Close();
            File.WriteAllLines(tempFile, ContasAManter);
            File.Delete(Caminho);
            File.Move(tempFile, Caminho);
        }
    }
}

    