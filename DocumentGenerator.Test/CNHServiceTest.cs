using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DocumentGenerator.Services;

namespace DocumentGenerator.Test
{
    public class CNHServiceTest
    {
        [Fact]
        public async Task CreateObjectCNHServiceAndCheckIsValid()
        {
            IDocumentService cnh = new CNHService();
            Assert.True(await cnh.IsValid("17546389219"));
        }

        [Fact]
        public async Task CreateObjectCNHServiceAndCheckNotIsValid()
        {
            IDocumentService cnh = new CNHService();
            Assert.False(await cnh.IsValid("17546389218"));
        }

        [Fact]
        public async Task CreateObjectCNHServiceAndCreateCPFAndAfterCheckIsValid()
        {
            IDocumentService cnh = new CNHService();
            var cnhCreate = await cnh.Create();
            Assert.True(await cnh.IsValid(cnhCreate));
        }

        [Fact]
        public async Task CreateObjectCNHServiceAndCreateListOfCPF()
        {
            IDocumentService cnh = new CNHService();
            var lista = await cnh.CreateList(100);
            Assert.True(lista.Count == 100);
        }
    }
}
