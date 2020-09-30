using System;
using System.Collections.Generic;
using System.Text;
using Corpnet.Services.Interfaces;
using AutoMapper;
using Corpnet.Entities;
using Corpnet.Data.Model;
using System.Linq;
using Newtonsoft.Json;
using Corpnet.Data.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace Corpnet.Services.Services
{
    public class DirectoryService : IDirectoryService
    {
        private readonly IMapper _mapper;
        private readonly IDirectory _repo;
        private readonly IConfiguration Configuration;

        public DirectoryService(IMapper mapper, IDirectory repo, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._repo = repo;
            Configuration = configuration;
        }

        public async Task<int> AddDirectory(DirectoryDto dirDto, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Directory>(dirDto);
            model.CreatedBy = dirDto.CreatedBy;
            model.ModifiedBy = dirDto.CreatedBy;
            model.Thumbnail =  dirDto.Parent_id == 0 ? Configuration["Thumbnail:RootFolder"] : Configuration["Thumbnail:SubFolder"];
            await _repo.AddDirectory(model, cancellationToken).ConfigureAwait(false);

            return 1;
        }
        public async Task<int> UpdateDirectory(int id, DirectoryDto dirDto, CancellationToken cancellationToken)
        {
            int ReturnId = 0;
            var model = await _repo.GetDirectoryById(id, cancellationToken);
            if (model != null)
            {
                int tempParentID = model.Parent_id;
                _mapper.Map(dirDto, model);
                model.ModifiedBy = dirDto.CreatedBy;
                model.ModifiedDate = DateTime.Now;
                model.Parent_id = tempParentID;
                model.Thumbnail = dirDto.Parent_id == 0 ? Configuration["Thumbnail:RootFolder"] : Configuration["Thumbnail:SubFolder"];
                await _repo.UpdateDirectory(model, cancellationToken).ConfigureAwait(false);
                ReturnId = 1;
            }
            return ReturnId;
        }
        public async Task<int> DeleteDirectory(int id, string username, CancellationToken cancellationToken)
        {
            int ReturnId = 0;
            var model = await _repo.GetDirectoryById(id, cancellationToken);
            if (model != null)
            {
                model.ModifiedBy = username;
                model.ModifiedDate = DateTime.Now;
                model.IsActive = false;
                await _repo.DeleteDirectory(model, cancellationToken).ConfigureAwait(false);
                ReturnId = 1;
            }
            return ReturnId;
        }

        public async Task<int> UpdateNavigation(IEnumerable<DirectoryNavDto> dirNav, CancellationToken cancellationToken)
        {
            int ReturnId = 0;

            var jsonString = JsonConvert.SerializeObject(dirNav);
            var result = await _repo.UpdateNavigationData(jsonString, cancellationToken).ConfigureAwait(false);

            string[] s = result.Select(p => p.result).ToArray();
            string res = s[0];
            if (res == "")
            {
                ReturnId = 0;
            }
            else
            {
                ReturnId = 1;
            }

            return ReturnId;
        }
        public async Task<string> GetDirectoryAsync(int id, string username)
        {
            //SQL Recursive method
            IEnumerable<GenericResult> data = await _repo.GetDirDoc(id, username).ConfigureAwait(false);

            string[] s = data.Select(p => p.result).ToArray();
            string jsonResponse = s[0];
            dynamic parsedJson = JsonConvert.DeserializeObject(jsonResponse);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);

        }
        public async Task<string> GetDirListMenuBar()
        {
            //SQL Recursive method
            IEnumerable<GenericResult> data = await _repo.GetDirListMenuBar().ConfigureAwait(false);

            string[] s = data.Select(p => p.result).ToArray();
            string jsonResponse = s[0];
            dynamic parsedJson = JsonConvert.DeserializeObject(jsonResponse);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }
        public async Task<Directory> GetDirById(int id, CancellationToken cancellationToken)
        {
            var model = await _repo.GetDirectoryById(id, cancellationToken);
            return model;
        }
        public async Task<int> ShowHideDirectory(int id, string ModifiedBy, CancellationToken cancellationToken)
        {
            int ReturnId = 0;
            var model = await _repo.GetDirectoryById(id, cancellationToken);
            if (model != null)
            {
                model.IsVisible = !model.IsVisible;
                model.ModifiedBy = ModifiedBy;
                model.ModifiedDate = DateTime.Now;
                await _repo.UpdateDirectory(model, cancellationToken).ConfigureAwait(false);
                ReturnId = 1;
            }
            return ReturnId;
        }



    }
}
