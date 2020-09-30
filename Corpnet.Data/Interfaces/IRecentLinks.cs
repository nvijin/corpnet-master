using Corpnet.Data.Model;
using Corpnet.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corpnet.Data.Interfaces
{
    public interface IRecentLinks
    {

        //IEnumerable<RecentLinks> GetRecentLinks(string username);

        Task<IEnumerable<Document>> SPGetRecentLinks(string username);
        Task AddRecentLink(RecentLinks recent, CancellationToken cancellationToken);
  
    }
}
