﻿using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp
{
    public static class ContaDataSource
    {
        static string Caminho = "C:\\Users\\pedro.dantas\\source\\repos\\Projeto_Pai\\ConsoleApp\\Contas\\Conta.txt";
        static string tempFile = "C:\\Users\\pedro.dantas\\source\\repos\\Projeto_Pai\\ConsoleApp\\tempContas\\Conta.txt";

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
                Conta conta = new Conta();
                conta.Titular = DadosConta[0].Split('=')[1];
                conta.Cpf = DadosConta[1].Split('=')[1];
                conta.Cidade = DadosConta[2].Split('=')[1];
                conta.Bairro = DadosConta[3].Split('=')[1];
                conta.Telefone = DadosConta[4].Split('=')[1];
                conta.Saldo = Convert.ToDouble(DadosConta[5].Split('=')[1]);
                conta.NumeroConta = DadosConta[6].Split('=')[1];
                conta.Agencia = DadosConta[7].Split('=')[1];

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

            List<string> ContasAManter = new List<string>() { };

            while (LerConta.EndOfStream != true)
            {
                string linha = LerConta.ReadLine();

                string[] DadosConta = linha.Split(',');
                Conta ContaAntiga = new Conta();
                ContaAntiga.Titular = DadosConta[0].Split('=')[1];
                ContaAntiga.Cpf = DadosConta[1].Split('=')[1];
                ContaAntiga.Cidade = DadosConta[2].Split('=')[1];
                ContaAntiga.Bairro = DadosConta[3].Split('=')[1];
                ContaAntiga.Telefone = DadosConta[4].Split('=')[1];
                ContaAntiga.Saldo = Convert.ToDouble(DadosConta[5].Split('=')[1]);
                ContaAntiga.NumeroConta = DadosConta[6].Split('=')[1];
                ContaAntiga.Agencia = DadosConta[7].Split('=')[1];

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

    