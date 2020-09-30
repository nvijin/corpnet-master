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
    public class DirectoryRepo : Repository<Directory>, IDirectory
    {
        private DataContext _ctx;

        public DirectoryRepo(DataContext ctx)
           : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<DirectoryModel>> GetMenu(string menuType)
        {
            return await _ctx.DirectoryModel.FromSqlRaw("[dbo].[spGetMenu] {0}", menuType).ToListAsync();
        }

        public async Task<IEnumerable<DirectoryModel>> GetDirectory(int id = 0)
        {
            return await _ctx.DirectoryModel.FromSqlRaw("[dbo].[spGetDirectoryAll] {0}", id).ToListAsync();
        }

        public async Task<IEnumerable<GenericResult>> GetDirDoc(int id, string username)
        {
            return await _ctx.GenericResult.FromSqlRaw("[dbo].[SPGetDirectoryDocument] {0},{1}", id, username).ToListAsync();
        }

        public async Task<IEnumerable<GenericResult>> GetDirListMenuBar()
        {
            return await _ctx.GenericResult.FromSqlRaw("[dbo].[SPGetDirectorySideBar]").ToListAsync();
        }

        public async Task<Directory> GetDirectoryById(int id, CancellationToken cancellationToken)
        {
           return await base.GetByIdAsync(cancellationToken, id);
        }
        
        public async Task AddDirectory(Directory dir, CancellationToken cancellationToken)
        {
            await base.AddAsync(dir, cancellationToken);
        }
        public async Task UpdateDirectory(Directory dir, CancellationToken cancellationToken)
        {
            await base.UpdateAsync(dir, cancellationToken);
        }
        public async Task DeleteDirectory(Directory dir, CancellationToken cancellationToken)
        {
            await base.UpdateAsync(dir, cancellationToken);
        }
        public async Task<List<GenericResult>> UpdateNavigationData(string UpdateNavData, CancellationToken cancellationToken)
        {

            return await _ctx.GenericResult.FromSqlRaw("[dbo].[sp_UpdateNavigation] {0}", UpdateNavData).ToListAsync();
            //return await _ctx.DirectoryNavDto.FromSqlRaw("[dbo].[SPGetDirectoryDocument] {0}", id).ToListAsync();
        }

        //public async Task ShowHideDirectory(Directory dir, CancellationToken cancellationToken)
        //{
        //    await base.UpdateAsync(dir, cancellationToken);
        //}


    }
}
