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
            var cpf = new Services.CPFService();
            Assert.True(await cpf.IsValid("60229466036"));
        }

        [Fact]
        public async Task CreateObjectCPFServiceAndCheckNotIsValid()
        {
            var cpf = new Services.CPFService();
            Assert.False(await cpf.IsValid("60229466032"));
        }

        [Fact]
        public async Task CreateObjectCPFServiceAndCreateCPFAndAfterCheckIsValid()
        {
            var cpf = new Services.CPFService();
            var cpfCreate = await cpf.Create();
            Assert.True(await cpf.IsValid(cpfCreate));
        }

        [Fact]
        public async Task CreateObjectCPFServiceAndCreateListOfCPF()
        {
            var cpf = new Services.CPFService();
            var lista = await cpf.CreateList(100);
            Assert.True(lista.Count == 100);
        }
    }
}
