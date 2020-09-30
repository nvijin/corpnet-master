using Corpnet.Data.Model;
using Corpnet.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corpnet.Data.Interfaces
{
    public interface IGeneric
    {
        Task<IEnumerable<GenericPage>> GetGenericPages(int id);
        Task<IEnumerable<GenericPageMenuModel>> GetGenericPagesAll(int id);

        Task<GenericPage> GetGenericPageById(int id, CancellationToken cancellationToken);
        Task AddGenericPage(GenericPage gen, CancellationToken cancellationToken);
        Task UpdateGenericPage(GenericPage gen, CancellationToken cancellationToken);
        Task DeleteGenericPage(GenericPage gen, CancellationToken cancellationToken);
        Task<IEnumerable<GenericResult>> GetUsers(string username);
        //   IEnumerable<RecentLinks> GetRecentLinks(string username);
        //Task AddRecentLink(RecentLinks recent, CancellationToken cancellationToken);

    }
}
