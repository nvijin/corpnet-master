using Corpnet.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Corpnet.Data.Interfaces;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Linq;
using Corpnet.Data.Model;
using Microsoft.Extensions.Configuration;
using System.Threading;
using Corpnet.Entities;

namespace Corpnet.Services.Services
{
    public class DocumentUploadService : IDocumentUploadService
    {

        private readonly IMapper _mapper;
        private readonly IDocumentUpload _repo;
        private readonly IConfiguration Configuration;


        public DocumentUploadService(IMapper mapper, IDocumentUpload repo, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._repo = repo;
            Configuration = configuration;

        }

        static class ConfigurationManager
        {


        }
        public async Task<string> GetDocumentUpload(int id, string RootDir)
        {
            //var data = await _repo.GetDocumentUploads(id).ConfigureAwait(false);
            IEnumerable<DocumentUpload> data = await _repo.GetDocumentUploads(id, RootDir).ConfigureAwait(false);

            string[] DocPath = data.Select(p => p.DocPath).ToArray();
            string CurrentPath = DocPath[0].ToString();

            string[] arr = data.Select(p => p.DocName).ToArray();

            string DirectoryName = RootDir;    // "C:\\TCIT\\Mashreq";


            foreach (string dir in arr)
            {
                // Console.WriteLine(dir);
                DirectoryName = DirectoryName + "\\" + dir;
                if (System.IO.Directory.Exists(DirectoryName))
                {
                    //Console.WriteLine("That path exists already.");
                    if (DirectoryName == DocPath[0])
                    {
                        Console.WriteLine("That path exists already.");
                    }

                }
                if (!System.IO.Directory.Exists(DirectoryName))

                {
                    DirectoryInfo di = System.IO.Directory.CreateDirectory(DirectoryName);
                    // Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(DirectoryName));
                }

                //// Delete the directory.
                //di.Delete();
                //Console.WriteLine("The directory was deleted successfully.");

            }

            //  string jsonResponse = arr[0];

            return DirectoryName;


            //string jsonMenu = JsonConvert.SerializeObject(data, Formatting.Indented);
            //return jsonMenu;
        }

  
        public async Task<int> AddDocument(DocumentDto Doc, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Document>(Doc);
            model.CreatedBy = Doc.CreatedBy;
            model.ModifiedBy = Doc.CreatedBy;
            model.Thumbnail = Configuration["Thumbnail:File"];
            await _repo.AddDocument(model, cancellationToken).ConfigureAwait(false);

            return 1;
        }

        public async Task<int> DeleteDocument(int id, string username, CancellationToken cancellationToken)
        {
            int ReturnId = 0;
            var model = await _repo.GetDocumentById(id, cancellationToken);
            if (model != null)
            {
                model.ModifiedBy = username;
                model.ModifiedDate = DateTime.Now;
                model.IsActive = false;
                await _repo.DeleteDocument(model, cancellationToken).ConfigureAwait(false);
                ReturnId = 1;
            }
            return ReturnId;
        }

        public async Task<int> UpdateDocument(int id, DocumentDto Doc, CancellationToken cancellationToken)
        {
            int ReturnId = 0;
            var model = await _repo.GetDocumentById(id, cancellationToken);
            if (model != null)
            {
                string DocPath = model.DocPath;
                int DocSize = model.DocSize;
                string DocType = model.DocType;
                string Thumbnail = model.Thumbnail;

                _mapper.Map(Doc, model);
                model.ModifiedBy = Doc.CreatedBy;
                model.ModifiedDate = DateTime.Now;
                model.DocName = Doc.DocName;
                model.DocDescription = Doc.DocDescription;
                model.Fk_Directory_id = Doc.Fk_Directory_id;
 
                model.DocSize = DocSize;
                model.DocType = DocType;
                model.Thumbnail = Thumbnail;
                model.DocPath = DocPath;
                model.Thumbnail = Configuration["Thumbnail:File"];

                await _repo.UpdateDocument(model, cancellationToken).ConfigureAwait(false);
                ReturnId = 1;
            }
            return ReturnId;
        }

        public async Task<string> GetBread(int id)
        {
            IEnumerable<DocumentUpload> data = await _repo.GetBreads(id).ConfigureAwait(false);
            string jsonMenu = JsonConvert.SerializeObject(data, Formatting.Indented);
            return jsonMenu;
        }


    }
}
