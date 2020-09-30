using AutoMapper;
using AutoMapper.QueryableExtensions;
using Corpnet.Data.Interfaces;
using Corpnet.Entities;
using Corpnet.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corpnet.Services.Services
{
    public class FavouriteService : IFavouriteService
    {
        private readonly IMapper _mapper;
        private readonly IFavourite _repo;

        public FavouriteService(IMapper mapper, IFavourite repo)
        {
            this._mapper = mapper;
            this._repo = repo;
        }

        public async Task<int> AddtoFavourite(FavouriteDto favDto, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Favourite>(favDto);
            model.CreatedBy = favDto.LDAPUser_id;
            model.ModifiedBy = favDto.LDAPUser_id;
            await _repo.AddtoFavourite(model, cancellationToken).ConfigureAwait(false);
            
            int FavId = 1; //Bind from
            return FavId;
        }

        public async Task<string> GetFavourite(string LDAPUser_id)
        {
            var data = await _repo.GetFavourite(LDAPUser_id).ConfigureAwait(false);
            string jsonMenu = JsonConvert.SerializeObject(data, Formatting.Indented);
            return jsonMenu;
        }
    }
}
