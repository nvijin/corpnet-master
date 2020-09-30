using Corpnet.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corpnet.Data.Interfaces
{
   public interface IFavourite
    {
        Task<IEnumerable<FavouriteVM>> GetFavourite(string LDAPUser_id);
        Task AddtoFavourite(Favourite fav, CancellationToken cancellationToken);
    }
}
