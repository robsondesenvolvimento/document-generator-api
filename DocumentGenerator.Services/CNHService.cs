using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentGenerator.Services
{
    public class CNHService : IDocumentService
    {
        private async Task<string> DigitCheckAndCreate(string document)
        {
            return await Task.Run(() =>
            {
                if (Regex.IsMatch(document, ".*[a-zA-Z\\.\\-]+.*")) throw new ArgumentException("CNH inválido, remova a mascara da CNH.");
                if (document.Length != 11) throw new ArgumentException("CNH inválido");

                var dsc = 0;
                var sum = 0;

                for (int i = 0, j = 9; i < 9; i++, j--)
                {
                    sum += (Convert.ToInt32(document[i].ToString()) * j);
                }

                var value1 = sum % 11;
                if (value1 >= 10)
{
                    value1 = 0;
                    dsc = 2;
                }

                sum = 0;
                for (int i = 0, j = 1; i < 9; ++i, ++j)
                {
                    sum += (Convert.ToInt32(document[i].ToString()) * j);
                }

                var x = sum % 11;

                var value2 = (x >= 10) ? 0 : x - dsc;

                return $"{document.Substring(0, 9)}{value1}{value2}";
            });
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
            var _cnh = await DigitCheckAndCreate(document).ConfigureAwait(false);
            return document == _cnh;
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
