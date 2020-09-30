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
    public class DocumentUploadRepo : Repository<Document>, IDocumentUpload
    {
        private DataContext _ctx;
   

        public DocumentUploadRepo(DataContext ctx)
                    : base(ctx)

        {
            _ctx = ctx;
        }
 


        public async Task<IEnumerable<DocumentUpload>> GetDocumentUploads (int id = 0, string RootDir="")
        {
            return await _ctx.DocumentUpload.FromSqlRaw("[dbo].[SP_CheckDirectory] {0}", id).ToListAsync();

        }

        public async Task<Document> GetDocumentById(int id, CancellationToken cancellationToken)
        {
            return await base.GetByIdAsync(cancellationToken, id);
        }

        public async Task AddDocument(Document Doc, CancellationToken cancellationToken)
        {
            await base.AddAsync(Doc, cancellationToken);
        }


        public async Task DeleteDocument(Document Doc, CancellationToken cancellationToken)
        {
            await base.UpdateAsync(Doc, cancellationToken);
        }

        public async Task UpdateDocument(Document Doc, CancellationToken cancellationToken)
        {
            await base.UpdateAsync(Doc, cancellationToken);
        }

        public async Task<IEnumerable<DocumentUpload>> GetBreads(int id = 0)
        {
            return await _ctx.DocumentUpload.FromSqlRaw("[dbo].[SP_GetBreadCrums] {0}", id).ToListAsync();
        }

    }
}
