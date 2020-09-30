using Corpnet.Data.Interfaces;
using Corpnet.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Corpnet.Entities;
using System.Threading;

namespace Corpnet.Data.Repos
{
    public class AdminRepo : Repository<AdminUsers>, IAdmin
    {
        private DataContext _ctx;

        public AdminRepo(DataContext ctx)
        : base(ctx)
        {
            _ctx = ctx;
        }
        public async Task<IEnumerable<AdminUsersVM>> GetAdminUsers(string id = "")
        {
            return await _ctx.AdminUsersVM.FromSqlRaw("[dbo].[spGetAdminUsers] {0}", id).ToListAsync();
        }

        public async Task<AdminUsers> GetAdminUsersById(int id, CancellationToken cancellationToken)
        {
            return await base.GetByIdAsync(cancellationToken, id);
        }

        public async Task<AdminUsers> GetAdminUsersByUsername(string LDAPUser_id, CancellationToken cancellationToken)
        {
           return await _ctx.AdminUsers.FirstOrDefaultAsync(x => x.LDAPUser_id == LDAPUser_id);
        }
        public async Task AddAdminUsers(AdminUsers adm, CancellationToken cancellationToken)
        {
            await base.AddAsync(adm, cancellationToken);
        }
        public async Task UpdateAdminUsers(AdminUsers adm, CancellationToken cancellationToken)
        {
            await base.UpdateAsync(adm, cancellationToken);
        }
        public async Task DeleteAdminUsers(AdminUsers adm, CancellationToken cancellationToken)
        {
            await base.UpdateAsync(adm, cancellationToken);
        }

        //public async Task<List<GenericResult>> UpdateNavigationData (string UpdateNavData, CancellationToken cancellationToken)
        //{

        //     return await _ctx.GenericResult.FromSqlRaw("[dbo].[sp_UpdateNavigation] {0}", UpdateNavData).ToListAsync();
        //    //return await _ctx.DirectoryNavDto.FromSqlRaw("[dbo].[SPGetDirectoryDocument] {0}", id).ToListAsync();
        //}

    }
}
