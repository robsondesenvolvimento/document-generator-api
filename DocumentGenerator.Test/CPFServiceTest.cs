using DocumentGenerator.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DocumentGenerator.Test
{
    public class CPFServiceTest
    {
        [Fact]
        public async Task CreateObjectCPFServiceAndCheckIsValid()
        {
            IDocumentService cpf = new CPFService();
            Assert.True(await cpf.IsValid("60229466036"));
        }

        [Fact]
        public async Task CreateObjectCPFServiceAndCheckNotIsValid()
        {
            IDocumentService cpf = new CPFService();
            Assert.False(await cpf.IsValid("60229466032"));
        }

        [Fact]
        public async Task CreateObjectCPFServiceAndCreateCPFAndAfterCheckIsValid()
        {
            IDocumentService cpf = new CPFService();
            var cpfCreate = await cpf.Create();
            Assert.True(await cpf.IsValid(cpfCreate));
        }

        [Fact]
        public async Task CreateObjectCPFServiceAndCreateListOfCPF()
        {
            IDocumentService cpf = new CPFService();
            var lista = await cpf.CreateList(100);
            Assert.True(lista.Count == 100);
        }
    }
}
