using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentGenerator.Services
{
    public interface IDocumentService
    {
        Task<string> Create();
        Task<bool> IsValid(string document);
        Task<List<string>> CreateList(int lenghtList);
    }
}
