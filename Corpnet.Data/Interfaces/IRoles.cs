using Corpnet.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corpnet.Data.Interfaces
{
    public interface IRoles
    {
        Task<IEnumerable<Roles>> GetRolesAsync();
        Task AddRole(Roles role, CancellationToken cancellationToken);
    }
}
