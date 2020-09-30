using Corpnet.Data.Model;
using Corpnet.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corpnet.Data.Interfaces
{
    public interface IDocumentUpload
    {
        Task<IEnumerable<DocumentUpload>> GetDocumentUploads(int id, string RootDir);

        Task<Document> GetDocumentById(int id, CancellationToken cancellationToken);
        Task AddDocument(Document Doc, CancellationToken cancellationToken);
        Task DeleteDocument(Document model, CancellationToken cancellationToken);
        Task UpdateDocument(Document model, CancellationToken cancellationToken);
        Task<IEnumerable<DocumentUpload>> GetBreads(int id);
    }
}

