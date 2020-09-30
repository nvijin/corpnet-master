using Corpnet.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Corpnet.Entities;
using System.Threading;

namespace Corpnet.Data.Interfaces
{
    public interface IAdmin
    {
        Task<IEnumerable<AdminUsersVM>> GetAdminUsers(string id);
        Task<AdminUsers> GetAdminUsersById(int id, CancellationToken cancellationToken);
        Task<AdminUsers> GetAdminUsersByUsername(string LDAPUser_id, CancellationToken cancellationToken);
        Task AddAdminUsers(AdminUsers adm,  CancellationToken cancellationToken);
        Task UpdateAdminUsers(AdminUsers adm, CancellationToken cancellationToken);
        Task DeleteAdminUsers(AdminUsers adm, CancellationToken cancellationToken);


    }
}
