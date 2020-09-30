
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
using System.Web;

namespace Corpnet.Services.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;
        private readonly IAdmin _repo;

        public AdminService(IMapper mapper, IAdmin repo)
        {
            this._mapper = mapper;
            this._repo = repo;
        }

        public async Task<string> GetAdminUsers(string id)
        {
            var data = await _repo.GetAdminUsers(id).ConfigureAwait(false);

            string jsonMenu = JsonConvert.SerializeObject(data, Formatting.Indented);
            return jsonMenu;
        }

        public async Task<int> AddAdminUsers(AdminUsersDto admDto, CancellationToken cancellationToken)
        {
            int ReturnId = 0;
            var modelExist = await _repo.GetAdminUsersByUsername(admDto.LDAPUser_id, cancellationToken);
            if (modelExist == null)
            {
                var model = _mapper.Map<AdminUsers>(admDto);
                model.CreatedBy = admDto.CreatedBy;
                model.ModifiedBy = admDto.CreatedBy;
                await _repo.AddAdminUsers(model, cancellationToken).ConfigureAwait(false);
                ReturnId = 1;
            }
            return ReturnId;
        }
        public async Task<int> UpdateAdminUsers(int id, AdminUsersDto admDto, CancellationToken cancellationToken)
        {
            int ReturnId = 0;
            var model = await _repo.GetAdminUsersById(id, cancellationToken);
            if (model != null)
            {
                _mapper.Map(admDto, model);
                model.ModifiedBy = admDto.CreatedBy;
                model.ModifiedDate = DateTime.Now;
                await _repo.UpdateAdminUsers(model, cancellationToken).ConfigureAwait(false);
                ReturnId = 1;
            }
            return ReturnId;
        }
        public async Task<int> DeleteAdminUsers(int id, string username, CancellationToken cancellationToken)
        {
            int ReturnId = 0;
            var model = await _repo.GetAdminUsersById(id, cancellationToken);
            if (model != null)
            {
                model.ModifiedBy = username;
                model.ModifiedDate = DateTime.Now;
                model.IsActive = false;
                await _repo.DeleteAdminUsers(model, cancellationToken).ConfigureAwait(false);
                ReturnId = 1;
            }
            return ReturnId;
        }


    }
}
