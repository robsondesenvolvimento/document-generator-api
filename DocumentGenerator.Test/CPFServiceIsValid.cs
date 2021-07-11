using DocumentGenerator.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DocumentGenerator.Test
{
    public class CPFServiceIsValid
    {
        [Fact]
        public async Task CreateObjectCPFServiceAndCheckIsValid()
        {
            var cpf = new CPFService();
            Assert.True(await cpf.IsValid("60229466036"));
        }

        [Fact]
        public async Task CreateObjectCPFServiceAndCheckNotIsValid()
        {
            var cpf = new CPFService();
            Assert.False(await cpf.IsValid("60229466032"));
        }

        [Fact]
        public async Task CreateObjectCPFServiceAndCreateCPFAndAfterCheckIsValid()
        {
            var cpf = new CPFService();
            var cpfCreate = await cpf.CreateCPF();
            Assert.True(await cpf.IsValid(cpfCreate));
        }

        [Fact]
        public async Task CreateObjectCPFServiceAndCreateListOfCPF()
        {
            var cpf = new CPFService();
            var lista = await cpf.CreateListCPF(100);
            Assert.True(lista.Count == 100);
        }
    }
}
