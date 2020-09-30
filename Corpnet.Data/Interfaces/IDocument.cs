using Corpnet.Data.Model;
using Corpnet.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corpnet.Data.Interfaces
{
    public interface IDocument
    {
        Task<IEnumerable<DocumentModel>> GetNotification();
        Task<IEnumerable<DocumentModel>> GetSearchResult(string query);
        Task<IEnumerable<DocumentModel>> GetFilesById(int id);
        Task ShowHideDocument(Document doc, CancellationToken cancellationToken);
        Task<Document> GetDocumentById(int id, CancellationToken cancellationToken);

    }
}
