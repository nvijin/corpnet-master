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

namespace Corpnet.Services.Services
{
    public class RecentLinksService : IRecentLinksService
    {

        private readonly IMapper _mapper;
        private readonly IRecentLinks _repo;

        public RecentLinksService(IMapper mapper, IRecentLinks repo)
        {
            this._mapper = mapper;
            this._repo = repo;

        }

        //public string GetRecentLinks(string username)
        //{
        //    var data = _repo.GetRecentLinks(username);

        //    string jsonMenu = JsonConvert.SerializeObject(data, Formatting.Indented);
        //    return jsonMenu;
        //}
        public async Task<string> SPGetRecentLinks(string username)
        {
            var data = await _repo.SPGetRecentLinks(username).ConfigureAwait(false);

            string jsonMenu = JsonConvert.SerializeObject(data, Formatting.Indented);
            return jsonMenu;
        }

        public async Task<int> AddRecentLink(RecentLinksDto recentDto, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<RecentLinks>(recentDto);
            model.CreatedBy = recentDto.CreatedBy;
            model.ModifiedBy = recentDto.CreatedBy;
            await _repo.AddRecentLink(model, cancellationToken).ConfigureAwait(false);

            return 1;
        }


    }
}
