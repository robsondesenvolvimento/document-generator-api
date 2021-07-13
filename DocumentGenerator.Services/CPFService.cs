using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentGenerator.Services
{
    public class CPFService : IDocumentService
    {
        private async Task<string> DigitCheckAndCreate(string document)
        {
            return await Task.Run(() =>
            {
                if (Regex.IsMatch(document, ".*[a-zA-Z\\.\\-]+.*")) throw new ArgumentException("CPF inválido, remova a mascara do CPF.");
                if (document.Length != 11) throw new ArgumentException("CPF inválido");                

                var _cpf = document;

                List<int> _digitosCpf = new();

                document.Substring(0, 9).ToList().ForEach(digito =>
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

        public async Task<string> Create()
        {
            return await Task.Run(async () =>
            {
                Random numeroAleatorio = new Random();
                var noveDigitosAleatorios = numeroAleatorio.Next(111111111, 999999999).ToString() +
                    numeroAleatorio.Next(11, 99).ToString();
                return await DigitCheckAndCreate(noveDigitosAleatorios);
            }).ConfigureAwait(false);            
        }

        public async Task<bool> IsValid(string document)
        {
            var _cpf = await DigitCheckAndCreate(document).ConfigureAwait(false);
            return document == _cpf;
        }

        public async Task<List<string>> CreateList(int lenghtList)
        {
            if (lenghtList > 100) throw new Exception("Max lenght is 100.");

            return await Task.Run(async () =>
            {
                var lista = new List<string>();
                for (int i = 0; i < lenghtList; i++)
                    lista.Add(await Create());
                return lista;
            });            
        }
    }
}
