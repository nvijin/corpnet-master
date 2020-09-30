using Corpnet.Data.Interfaces;
using Corpnet.Data.Model;
using Corpnet.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corpnet.Data.Repos
{
    public class DocumentRepo : Repository<Document>, IDocument
    {
        private DataContext _ctx;

        public DocumentRepo(DataContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<DocumentModel>> GetNotification()
        {
            return await _ctx.DocumentModel.FromSqlRaw("[dbo].[spGetNotification]").ToListAsync();
        }

        public async Task<IEnumerable<DocumentModel>> GetSearchResult(string query)
        {
            return await _ctx.DocumentModel.FromSqlRaw("[dbo].[spSearchDocument] {0}", query).ToListAsync();
        }

        public async Task<IEnumerable<DocumentModel>> GetFilesById(int Fk_Directory_id)
        {
            return await _ctx.DocumentModel.FromSqlRaw("[dbo].[spGetFilesById] {0}", Fk_Directory_id).ToListAsync();
        }

        public async Task<Document> GetDocumentById(int id, CancellationToken cancellationToken)
        {
            return await base.GetByIdAsync(cancellationToken, id);
        }

        public async Task ShowHideDocument(Document doc, CancellationToken cancellationToken)
        {
            await base.UpdateAsync(doc, cancellationToken);
        }

    }
}
