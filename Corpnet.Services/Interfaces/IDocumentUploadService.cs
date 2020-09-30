

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Corpnet.Entities;

namespace Corpnet.Services.Interfaces
{
   public interface IDocumentUploadService
    {
        Task<string> GetDocumentUpload(int id, string RootDir);
        Task<int> AddDocument(DocumentDto Doc, CancellationToken cancellationToken);

        Task<int> DeleteDocument(int id, string username, CancellationToken cancellationToken);
        Task<int> UpdateDocument(int id, DocumentDto Doc, CancellationToken cancellationToken);
        Task<string> GetBread(int id);
    }
}
