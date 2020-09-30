using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Corpnet.Entities;

namespace Corpnet.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<string> GetNotification();
        Task<string> GetSearchResult(string query);
        Task<string> GetFilesById(int id);
        Task<int> ShowHideDocument(int id, string ModifiedBy, CancellationToken cancellationToken);
        string GetMime(string filename);
        Task<string> GetFileById(int id, CancellationToken cancellationToken);
    }
}
