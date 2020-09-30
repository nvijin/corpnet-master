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
    public class AdminDirectoryAccessRepo : Repository<AdminDirectoryAccess>, IAdminDirectoryAccess
    {
        private DataContext _ctx;

        public AdminDirectoryAccessRepo(DataContext ctx)
        : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<AdminDirectoryAccess> GetAdminDirectoryAccessById(int id, CancellationToken cancellationToken)
        {
            return await base.GetByIdAsync(cancellationToken, id);
        }


        public async Task AddAdminDirectoryAccess(AdminDirectoryAccess admdir, CancellationToken cancellationToken)
        {
            await base.AddAsync(admdir, cancellationToken);
        }
        public async Task UpdateAdminDirectoryAccess(AdminDirectoryAccess admdir, CancellationToken cancellationToken)
        {
           await base.UpdateAsync(admdir, cancellationToken);

        }
        public async Task DeleteAdminDirectoryAccess(AdminDirectoryAccess admdir, CancellationToken cancellationToken)
        {
            await base.UpdateAsync(admdir, cancellationToken);
        }

        public async Task<List<GenericResult>> UpdateDirData(string UpdateData, CancellationToken cancellationToken)
        {
            //Add folders to admin user
            return await _ctx.GenericResult.FromSqlRaw("[dbo].[sp_AdminDirectoryAccess] {0}", UpdateData).ToListAsync();
        }

        public async Task<IEnumerable<GenericResult>> GetAdminDirData(string UserID)
        {
            //Get folders by admin user
            return await _ctx.GenericResult.FromSqlRaw("[dbo].[SPGetAdminDirectoryAccess] {0}", UserID).ToListAsync();
        }


    }
}
