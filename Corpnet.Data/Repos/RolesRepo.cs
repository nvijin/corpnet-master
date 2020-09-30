using Corpnet.Data.Interfaces;
using Corpnet.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corpnet.Data.Repos
{
    public class RolesRepo : Repository<Roles>, IRoles
    {
        private DataContext _ctx;

        public RolesRepo(DataContext ctx) 
            : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<Roles>> GetRolesAsync()
        {
            return await _ctx.RoleMaster.FromSqlInterpolated($"[dbo].[spGetRoleMaster]").ToListAsync();
        }
        public async Task AddRole(Roles role, CancellationToken cancellationToken)
        {
             await base.AddAsync(role, cancellationToken);
        }

    }
}
