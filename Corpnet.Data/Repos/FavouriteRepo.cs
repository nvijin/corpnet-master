using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Corpnet.Data.Interfaces;
using Corpnet.Entities;
using Microsoft.EntityFrameworkCore;

namespace Corpnet.Data.Repos
{
    public class FavouriteRepo : Repository<Favourite>, IFavourite
    {
        private DataContext _ctx;

        public FavouriteRepo(DataContext ctx)
            : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task AddtoFavourite(Favourite fav, CancellationToken cancellationToken)
        {

           var favResult = _ctx.Favourite
                .Where(x => x.fk_DocDir_id == fav.fk_DocDir_id)
                .Where(x => x.DocDirType == fav.DocDirType)
                .Where(x => x.LDAPUser_id == fav.LDAPUser_id).FirstOrDefault();

            if (favResult == null) //New Favourite Record
            {
                await base.AddAsync(fav, cancellationToken);
            }
            else //Update Existing Favourite Record
            {
                favResult.IsActive = !favResult.IsActive;
                favResult.ModifiedDate = DateTime.Now;
                await base.UpdateAsync(favResult, cancellationToken);
            }
        }
        public async Task<IEnumerable<FavouriteVM>> GetFavourite(string LDAPUser_id)
        {
            return await _ctx.FavouriteVM.FromSqlRaw("[dbo].[spGetFavourite] {0}", LDAPUser_id).ToListAsync();
        }

    }
}
