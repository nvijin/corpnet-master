using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Corpnet.Entities;

namespace Corpnet.Services.Interfaces
{
   public interface IDirectoryService
    {
        Task<string> GetDirectoryAsync(int id, string username);
        Task<string> GetDirListMenuBar();
        Task<Directory> GetDirById(int id, CancellationToken cancellationToken);
        Task<int> AddDirectory(DirectoryDto dir, CancellationToken cancellationToken);
        Task<int> UpdateDirectory(int id,DirectoryDto dir, CancellationToken cancellationToken);
        Task<int> DeleteDirectory(int id, string username, CancellationToken cancellationToken);
        Task<int> UpdateNavigation(IEnumerable<DirectoryNavDto> dirList, CancellationToken cancellationToken);
        Task<int> ShowHideDirectory(int id, string ModifiedBy, CancellationToken cancellationToken);
    }
}
