using System;
using System.IO;

namespace ConsoleApp
{
    public static class ContaDataSource
    {
        static string Caminho = "C:\\Users\\pedro.dantas\\source\\repos\\Projeto_Pai\\ConsoleApp\\Contas\\Conta.txt";

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
                Console.WriteLine("Não foi possivel criar a conta no momento!");
                return false;
            }
        }

        public static void SaveExtrato(Conta conta, string menssagem)
        {
            string CaminhoExtrato = "C:\\" + "Users\\" + "pedro.dantas\\" + "source\\" + "repos\\" + "Projeto_Pai\\" + "ConsoleApp\\" + "ExtratoContas\\";
            string NomeArquivo = $"{conta.Agencia}{conta.NumeroConta}";
            FileInfo umArquivo = new FileInfo(CaminhoExtrato+NomeArquivo);
 
            try
            {
                DateTime now = DateTime.Now;
                StreamWriter AtualizarExtrato = umArquivo.AppendText();
                {
                    AtualizarExtrato.WriteLine($"Hora = {now} => {menssagem}");
                    AtualizarExtrato.Close();
                }
            }
            catch
            {
                Console.WriteLine("Não foi possivel salvar o extrato!");
            }
            
        }
            public static Conta GetByAgenciaEConta(string InputAgencia, string InputNumeroConta)
            {
            StreamReader LerConta = File.OpenText(Caminho);

            while (LerConta.EndOfStream != true)
            {
                string linha = LerConta.ReadLine();
                
                string[] DadosConta = linha.Split(',');
                Conta pp = new Conta();
                pp.Titular = DadosConta[0].Split('=')[1];
                pp.Cpf = DadosConta[1].Split('=')[1];
                pp.Cidade = DadosConta[2].Split('=')[1];
                pp.Bairro = DadosConta[3].Split('=')[1];
                pp.Telefone = DadosConta[4].Split('=')[1];
                pp.Saldo = Convert.ToDouble(DadosConta[5].Split('=')[1]);
                pp.NumeroConta = DadosConta[6].Split('=')[1];
                pp.Agencia = DadosConta[7].Split('=')[1];

                if (pp.Agencia == InputAgencia && pp.NumeroConta == InputNumeroConta)
                {
                    LerConta.Close();
                    return pp;
                }
            }
            LerConta.Close();
            return null;
        }
    }
}

    