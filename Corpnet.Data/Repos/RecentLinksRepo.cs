using Corpnet.Data.Interfaces;
using Corpnet.Data.Model;
using Corpnet.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corpnet.Data.Repos
{
    public class RecentLinksRepo : Repository<RecentLinks>, IRecentLinks
    {
        private DataContext _ctx;


        public RecentLinksRepo(DataContext ctx) 
             : base(ctx) 

        {
            _ctx = ctx;
        }


        //public  IEnumerable<RecentLinks> GetRecentLinks(string username)
        //{
        //   return _ctx.RecentLinks.Where(x => x.LDAPUser_id == username).ToList();
        //}

        public async Task<IEnumerable<Document>> SPGetRecentLinks(string username="")
        {
            return await _ctx.Document.FromSqlRaw("[dbo].[spGetRecentLinks] {0}", username).ToListAsync();
        }

        public async Task AddRecentLink(RecentLinks recent, CancellationToken cancellationToken)
        {
            await base.AddAsync(recent, cancellationToken);
        }

    }
}
