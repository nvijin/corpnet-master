
using Corpnet.Data.Model;
using Corpnet.Entities;
using Corpnet.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corpnet.Services.Interfaces
{
   public interface IGenericService
    {
        Task<string> GetGenericPage(int id);
        Task<string> GetGenericPageAll(int id);
        Task<int> AddGenericPage(GenericPageDto dir, CancellationToken cancellationToken);
        Task<int> UpdateGenericPage(int id, GenericPageDto dir, CancellationToken cancellationToken);
        Task<int> DeleteGenericPage(int id, string username, CancellationToken cancellationToken);
        Task<IEnumerable<GenericResult>> GetUser(string username);
        //string GetRecentLinks(string username);
        //Task<int> AddRecentLink(RecentLinksDto recentDto, CancellationToken cancellationToken);

    }
}
