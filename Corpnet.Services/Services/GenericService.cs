using Corpnet.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Corpnet.Data.Interfaces;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using Corpnet.Entities;
using Corpnet.Data.Model;

namespace Corpnet.Services.Services
{
    public class GenericService : IGenericService
    {

        private readonly IMapper _mapper;
        private readonly IGeneric _repo;
        private readonly IErrorlogService _errorlogService;

        public GenericService(IMapper mapper, IGeneric repo, IErrorlogService errorlogService)
        {
            this._mapper = mapper;
            this._repo = repo;
            this._errorlogService = errorlogService;
        }


        public async Task<string> GetGenericPage(int id)
        {
            var data = await _repo.GetGenericPages(id).ConfigureAwait(false);

            string jsonMenu = JsonConvert.SerializeObject(data, Formatting.Indented);
            return jsonMenu;
        }
        public async Task<string> GetGenericPageAll(int id)
        {
            var data = await _repo.GetGenericPagesAll(id).ConfigureAwait(false);

            string jsonMenu = JsonConvert.SerializeObject(data, Formatting.Indented);
            return jsonMenu;
        }


        public async Task<int> AddGenericPage(GenericPageDto genDto, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<GenericPage>(genDto);
            model.CreatedBy = genDto.CreatedBy;
            model.ModifiedBy = genDto.CreatedBy;
            await _repo.AddGenericPage(model, cancellationToken).ConfigureAwait(false);

            return 1;
        }
        public async Task<int> UpdateGenericPage(int id, GenericPageDto genDto, CancellationToken cancellationToken)
        {
            int ReturnId = 0;
            var model = await _repo.GetGenericPageById(id, cancellationToken);
            if (model != null)
            {
                _mapper.Map(genDto, model);
                model.ModifiedBy = genDto.CreatedBy;
                model.ModifiedDate = DateTime.Now;
                await _repo.UpdateGenericPage(model, cancellationToken).ConfigureAwait(false);
                ReturnId = 1;
            }
            return ReturnId;
        }
        public async Task<int> DeleteGenericPage(int id, string username, CancellationToken cancellationToken)
        {
            int ReturnId = 0;
            var model = await _repo.GetGenericPageById(id, cancellationToken);
            if (model != null)
            {
                model.ModifiedBy = username;
                model.ModifiedDate = DateTime.Now;
                model.IsActive = false;
                await _repo.DeleteGenericPage(model, cancellationToken).ConfigureAwait(false);
                ReturnId = 1;
            }
            return ReturnId;
        }

        //public string GetRecentLinks(string username)
        //{
        //    var data = _repo.GetRecentLinks(username);

        //    string jsonMenu = JsonConvert.SerializeObject(data, Formatting.Indented);
        //    return jsonMenu;
        //}


        //public async Task<int> AddRecentLink(RecentLinksDto recentDto, CancellationToken cancellationToken)
        //{
        //    var model = _mapper.Map<RecentLinks>(recentDto);
        //    model.CreatedBy = recentDto.CreatedBy;
        //    model.ModifiedBy = recentDto.CreatedBy;
        //    await _repo.AddRecentLink(model, cancellationToken).ConfigureAwait(false);

        //    return 1;
        //}
        public async Task<IEnumerable<GenericResult>> GetUser(string username)
        {
            return await _repo.GetUsers(username).ConfigureAwait(false);
        }


    }
}
