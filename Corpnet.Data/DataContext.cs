using System;
using System.Collections.Generic;
using System.Text;
using Corpnet.Data.Model;
using Microsoft.EntityFrameworkCore;
using Corpnet.Entities;
using Corpnet.Entities.Model;

namespace Corpnet.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }        
        
        public DbSet<Directory> Directory { get; set; }
        public DbSet<DirectoryModel> DirectoryModel { get; set; }
        public DbSet<DocumentModel> DocumentModel { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<DocumentUpload> DocumentUpload { get; set; }
        public DbSet<GenericResult> GenericResult { get; set; }
        public DbSet<GenericPageModel> GenericPageModel { get; set; }
        public DbSet<GenericPageMenuModel> GenericPageMenuModel { get; set; }
        public DbSet<GenericPage> GenericPage { get; set; }
        public DbSet<Favourite> Favourite { get; set; }
        public DbSet<FavouriteVM> FavouriteVM { get; set; }
        public DbSet<Roles> RoleMaster { get; set; }

        public DbSet<RecentLinks> RecentLinks { get; set; }
        public DbSet<AdminUsers> AdminUsers { get; set; }
        public DbSet<AdminUsersVM> AdminUsersVM { get; set; }
        public DbSet<AdminDirectoryAccess> AdminDirectoryAccess { get; set; }
        public DbSet<ErrorLog> ErrorLog { get; set; }

    }
}
