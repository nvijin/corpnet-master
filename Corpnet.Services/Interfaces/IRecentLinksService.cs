
using Corpnet.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corpnet.Services.Interfaces
{
   public interface IRecentLinksService
    {

        //string GetRecentLinks(string username);
        Task<string> SPGetRecentLinks(string username);
        Task<int> AddRecentLink(RecentLinksDto recentDto, CancellationToken cancellationToken);

    }
}
