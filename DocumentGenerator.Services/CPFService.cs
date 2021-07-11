using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentGenerator.Services
{
    public class CPFService
    {
        private async Task<string> DigitCheckAndCreate(string cpf)
        {
            return await Task.Run(() =>
            {
                if (Regex.IsMatch(cpf, ".*[a-zA-Z\\.\\-]+.*")) throw new ArgumentException("CPF inválido, remova a mascara do CPF.");
                if (cpf.Length != 11) throw new ArgumentException("CPF inválido");                

                var _cpf = cpf;

                List<int> _digitosCpf = new();

                cpf.Substring(0, 9).ToList().ForEach(digito =>
                {
                    var digitoAtual = Int32.Parse(digito.ToString());
                    _digitosCpf.Add(digitoAtual);
                });

                var multiplicador = 10;
                var soma = 0;

                _digitosCpf.ForEach(digito =>
                {
                    soma += digito * multiplicador;
                    multiplicador--;
                });

                var decimoDigito = 11 - (soma % 11);

                if (decimoDigito > 9)
                    decimoDigito = 0;

                _digitosCpf.RemoveAt(0);
                _digitosCpf.Add(decimoDigito);

                multiplicador = 10;
                soma = 0;

                _digitosCpf.ForEach(digito =>
                {
                    soma += digito * multiplicador;
                    multiplicador--;
                });

                var decimoPrimeiroDigito = 11 - (soma % 11);

                if (decimoPrimeiroDigito > 9)
                    decimoPrimeiroDigito = 0;

                return $"{_cpf.Substring(0, 9)}{decimoDigito}{decimoPrimeiroDigito}";
            }).ConfigureAwait(false);
        }

        public async Task<string> CreateCPF()
        {
            return await Task.Run(async () =>
            {
                Random numeroAleatorio = new Random();
                var noveDigitosAleatorios = numeroAleatorio.Next(111111111, 999999999).ToString() +
                    numeroAleatorio.Next(11, 99).ToString();
                return await DigitCheckAndCreate(noveDigitosAleatorios);
            }).ConfigureAwait(false);            
        }

        public async Task<bool> IsValid(string cpf)
        {
            var _cpf = await DigitCheckAndCreate(cpf).ConfigureAwait(false);
            return cpf == _cpf;
        }

        public async Task<List<string>> CreateListCPF(int lenghtList)
        {
            if (lenghtList > 100) throw new Exception("Max lenght is 100.");

            return await Task.Run(async () =>
            {
                var lista = new List<string>();
                for (int i = 0; i < lenghtList; i++)
                    lista.Add(await CreateCPF());
                return lista;
            });            
        }
    }
}
