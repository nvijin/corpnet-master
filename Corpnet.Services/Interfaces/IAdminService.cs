using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Corpnet.Entities;

namespace Corpnet.Services.Interfaces
{
   public interface IAdminService
    {
        Task<string> GetAdminUsers(string id);
        Task<int> AddAdminUsers(AdminUsersDto admdto, CancellationToken cancellationToken);
        Task<int> UpdateAdminUsers(int id, AdminUsersDto admdto, CancellationToken cancellationToken);
        Task<int> DeleteAdminUsers(int id, string username, CancellationToken cancellationToken);
        //Task<int> UpdateNavigation(IEnumerable<DirectoryNavDto> dirList, CancellationToken cancellationToken);

    }
}
