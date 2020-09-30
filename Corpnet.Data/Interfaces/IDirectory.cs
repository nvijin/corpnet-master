using Corpnet.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Corpnet.Entities;
using System.Threading;

namespace Corpnet.Data.Interfaces
{
    public interface IDirectory
    {
        Task<IEnumerable<DirectoryModel>> GetMenu(string menuType);
        Task<IEnumerable<DirectoryModel>> GetDirectory(int id);
        Task<IEnumerable<GenericResult>> GetDirDoc(int id, string username);
        Task<IEnumerable<GenericResult>> GetDirListMenuBar();
        Task<Directory> GetDirectoryById(int id, CancellationToken cancellationToken);
        Task AddDirectory(Directory dir, CancellationToken cancellationToken);
        Task UpdateDirectory(Directory dir, CancellationToken cancellationToken);
        Task DeleteDirectory(Directory dir, CancellationToken cancellationToken);
        Task<List<GenericResult>> UpdateNavigationData(string UpdateNavData, CancellationToken cancellationToken);
        //Task ShowHideDirectory(Directory dir, CancellationToken cancellationToken);
    }
}
