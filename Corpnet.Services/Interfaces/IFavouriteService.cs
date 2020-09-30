using Corpnet.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corpnet.Services.Interfaces
{
    public interface IFavouriteService
    {
        Task<string> GetFavourite(string LDAPUser_id);

        Task<int> AddtoFavourite(FavouriteDto favDto, CancellationToken cancellationToken);
    }
}
