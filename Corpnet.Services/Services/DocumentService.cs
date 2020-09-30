using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Corpnet.Data.Interfaces;
using Corpnet.Services.Interfaces;
using Newtonsoft.Json;
using Corpnet.Entities;
using HeyRed.Mime;

namespace Corpnet.Services.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IMapper mapper;
        private readonly IDocument _repo;

        public DocumentService(IMapper mapper, IDocument repo)
        {
            this.mapper = mapper;
            this._repo = repo;
        }

        public async Task<string> GetNotification()
        {
            var data = await _repo.GetNotification().ConfigureAwait(false);

            string jsonMenu = JsonConvert.SerializeObject(data, Formatting.Indented);
            return jsonMenu;
        }


        public async Task<string> GetSearchResult(string query)
        {
            var data = await _repo.GetSearchResult(query).ConfigureAwait(false);

            string jsonMenu = JsonConvert.SerializeObject(data, Formatting.Indented);
            return jsonMenu;
        }

        public async Task<string>  GetFilesById(int id)
        {
            var data = await _repo.GetFilesById(id).ConfigureAwait(false);

            string jsonMenu = JsonConvert.SerializeObject(data, Formatting.Indented);
            return jsonMenu;
        }

        public async Task<int> ShowHideDocument(int id, string ModifiedBy, CancellationToken cancellationToken)
        {
            int ReturnId = 0;
            var model = await _repo.GetDocumentById(id, cancellationToken);
            if (model != null)
            {
                model.IsVisible = !model.IsVisible;
                model.ModifiedBy = ModifiedBy;
                model.ModifiedDate = DateTime.Now;
                await _repo.ShowHideDocument(model, cancellationToken).ConfigureAwait(false);
                ReturnId = 1;
            }
            return ReturnId;
        }

        public string GetMime(string filename)
        {
            string MimeType = MimeTypesMap.GetMimeType(filename);
            return MimeType;
        }

        public async Task<string> GetFileById(int id, CancellationToken cancellationToken)
        {
            string filePath = "";
            var model = await _repo.GetDocumentById(id, cancellationToken);
            if (model != null)
                filePath = model.DocPath;
            
            return filePath;
        }


    }
}
