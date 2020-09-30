using Corpnet.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Corpnet.Entities;
using System.Threading;

namespace Corpnet.Data.Interfaces
{
    public interface IAdminDirectoryAccess
    {
 
        Task<AdminDirectoryAccess> GetAdminDirectoryAccessById(int id, CancellationToken cancellationToken);
        Task AddAdminDirectoryAccess(AdminDirectoryAccess admdir, CancellationToken cancellationToken);
        Task UpdateAdminDirectoryAccess(AdminDirectoryAccess admdir, CancellationToken cancellationToken);
        Task DeleteAdminDirectoryAccess(AdminDirectoryAccess admdir, CancellationToken cancellationToken);

        Task<List<GenericResult>> UpdateDirData(string UpdateDirData, CancellationToken cancellationToken);
        Task<IEnumerable<GenericResult>> GetAdminDirData(string UserID);
    }
}
