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
    public class GenericRepo : Repository<GenericPage>, IGeneric
    {
        private readonly DataContext _ctx;   

        public GenericRepo(DataContext ctx) 
             : base(ctx) 

        {
            _ctx = ctx;
        }

       
        public async Task<IEnumerable<GenericPage>> GetGenericPages(int id = 0)
        {
            return await _ctx.GenericPage.FromSqlRaw("[dbo].[spGenericPage] {0}", id).ToListAsync();
        }

        public async Task<IEnumerable<GenericPageMenuModel>> GetGenericPagesAll(int id = 0)
        {
            return await _ctx.GenericPageMenuModel.FromSqlRaw("[dbo].[spGenericPageAll] {0}", id).ToListAsync();
        }

        public async Task<GenericPage> GetGenericPageById(int id, CancellationToken cancellationToken)
        {
            return await base.GetByIdAsync(cancellationToken, id);
        }

        public async Task AddGenericPage(GenericPage gen, CancellationToken cancellationToken)
        {
            await base.AddAsync(gen, cancellationToken);
        }
        public async Task UpdateGenericPage(GenericPage gen, CancellationToken cancellationToken)
        {
            await base.UpdateAsync(gen, cancellationToken);
        }
        public async Task DeleteGenericPage(GenericPage gen, CancellationToken cancellationToken)
        {
            await base.UpdateAsync(gen, cancellationToken);
        }
        //public IEnumerable<RecentLinks> GetRecentLinks(string username)
        //{
        //    return _ctx.RecentLinks.Where(x => x.LDAPUser_id == username).ToList();
        //}

        //public async Task AddRecentLink(RecentLinks recent, CancellationToken cancellationToken)
        //{
        //    await _ctx.RecentLinks.AddAsync(recent, cancellationToken);     

        //}
        public async Task<IEnumerable<GenericResult>> GetUsers(string username)
        {
            return await _ctx.GenericResult.FromSqlRaw("[dbo].[sp_GetAdminUserById] {0}", username).ToListAsync();
        }

    }
}
