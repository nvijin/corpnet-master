using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Corpnet.Entities;

namespace Corpnet.Services.Interfaces
{
   public interface IAdminDirectoryAccessService
    {

        Task<int> AddAdminDirectoryAccess(IEnumerable<AdminDirectoryAccessDto> admdto, CancellationToken cancellationToken);
        Task<int> UpdateAdminDirectoryAccess(int id, AdminDirectoryAccessDto admdto, CancellationToken cancellationToken);
        Task<int> DeleteAdminDirectoryAccess(int id, string username, CancellationToken cancellationToken);
        //string GetAdminDirData(string UserID);
        Task<string> GetAdminDirData(string username);
    }
}
