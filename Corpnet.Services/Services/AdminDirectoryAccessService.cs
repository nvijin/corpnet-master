
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
    public class AdminDirectoryAccessService : IAdminDirectoryAccessService
    {
        private readonly IMapper _mapper;
        private readonly IAdminDirectoryAccess _repo;

        public AdminDirectoryAccessService(IMapper mapper, IAdminDirectoryAccess repo)
        {
            this._mapper = mapper;
            this._repo = repo;
        }


        public async Task<int> AddAdminDirectoryAccess(IEnumerable<AdminDirectoryAccessDto> admDto, CancellationToken cancellationToken)
        {
            //var model  = _mapper.Map<AdminDirectoryAccess>(admDto);
           //model.CreatedBy = admDto.CreatedBy;
           // model.ModifiedBy = admDto.CreatedBy;
           // await _repo.AddAdminDirectoryAccess(model, cancellationToken).ConfigureAwait(false);

            int ReturnId = 0;

            var jsonString = JsonConvert.SerializeObject(admDto);
            var result = await _repo.UpdateDirData(jsonString, cancellationToken).ConfigureAwait(false);

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
        public async Task<int> UpdateAdminDirectoryAccess(int id, AdminDirectoryAccessDto admDto, CancellationToken cancellationToken)
        {
            int ReturnId = 0;
            var model = await _repo.GetAdminDirectoryAccessById(id, cancellationToken);
            if (model != null)
            {
                _mapper.Map(admDto, model);
                model.ModifiedBy = admDto.CreatedBy;
                model.ModifiedDate = DateTime.Now;
             //   await _repo.UpdateAdminDirectoryAccess(admDto, cancellationToken).ConfigureAwait(false);
                ReturnId = 1;
            }
            return ReturnId;
        }
        public async Task<int> DeleteAdminDirectoryAccess(int id, string username, CancellationToken cancellationToken)
        {
            int ReturnId = 0;
            var model = await _repo.GetAdminDirectoryAccessById(id, cancellationToken);
            if (model != null)
            {
                model.ModifiedBy = username;
                model.ModifiedDate = DateTime.Now;
                model.IsActive = false;
                await _repo.DeleteAdminDirectoryAccess(model, cancellationToken).ConfigureAwait(false);
                ReturnId = 1;
            }
            return ReturnId;
        }



        //public string GetAdminDirData(string UserID);
        //{
        //    var data = _repo.GetAdminDirData(UserID);

        //    string jsonMenu = JsonConvert.SerializeObject(data, Formatting.Indented);
        //    return jsonMenu;
        //}
        public async Task<string> GetAdminDirData(string username)
        {
            var data = await _repo.GetAdminDirData(username).ConfigureAwait(false);

            
            //string jsonMenu = JsonConvert.SerializeObject(data, Formatting.Indented);
            //return jsonMenu;

            string[] s = data.Select(p => p.result).ToArray();
            string jsonResponse = s[0];
            dynamic parsedJson = JsonConvert.DeserializeObject(jsonResponse);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }

    }
}
