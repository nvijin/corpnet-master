using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Corpnet.Data.Interfaces;
using System.Collections.Generic;
using Corpnet.Services.Interfaces;
using Corpnet.Entities;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.Extensions;
namespace Corpnet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IAdminDirectoryAccessService _adminDirectoryAccessService;
        private readonly IErrorlogService _errorlogService;

        public AdminController(IAdminService adminService, IAdminDirectoryAccessService adminDirectoryAccessService, IErrorlogService errorlogService)
        {
            this._adminService = adminService;
            this._adminDirectoryAccessService = adminDirectoryAccessService;
            this._errorlogService = errorlogService;
        }

        [HttpGet("GetAdminUsers")]
        public async Task<IActionResult> GetAdminUsers(string id)
        {
            try
            {
                string dirJson = await _adminService.GetAdminUsers(id).ConfigureAwait(false);
                return Ok(dirJson);
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString(), cancellationToken).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }

        [HttpGet("GetAdminDirectory")]
        public async Task<IActionResult> GetAdminDirectory(string username)
        {
            try
            {
                string dirJson = await _adminDirectoryAccessService.GetAdminDirData(username).ConfigureAwait(false);
                return Ok(dirJson);
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString(), cancellationToken).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddAdminUsers(AdminUsersDto adm, CancellationToken cancellationToken)
        {
            try
            {
                int result = await _adminService.AddAdminUsers(adm, cancellationToken).ConfigureAwait(false);
                if (result == 0)
                    return BadRequest("User already exist");
                else
                    return Ok("Admin User Added Successfully");
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString(), cancellationToken).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAdminUsers(int id, AdminUsersDto adm, CancellationToken cancellationToken)
        {
            try
            {
            int result = await _adminService.UpdateAdminUsers(id, adm, cancellationToken).ConfigureAwait(false);
            if (result == 0)
                    return BadRequest("No data found!");
                else
                return Ok("Admin Users Updated Successfully");
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString(), cancellationToken).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }

        [HttpPut("Delete")]
        public async Task<IActionResult> DeleteAdminUsers(int id, string username, CancellationToken cancellationToken)
        {
            try
            {
            int result = await _adminService.DeleteAdminUsers(id, username, cancellationToken).ConfigureAwait(false);
            if (result == 0)
                    return BadRequest("No data found!");
                else
                return Ok("Admin Users Deleted Successfully");
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString(), cancellationToken).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }


        [HttpPost("AdminDirectory")]
        public async Task<IActionResult> AddAdminDirectoryAccess(IEnumerable<AdminDirectoryAccessDto> admdir, CancellationToken cancellationToken)
        {
            try
            {
            int result = await _adminDirectoryAccessService.AddAdminDirectoryAccess(admdir, cancellationToken).ConfigureAwait(false);
            if (result == 0)
                return BadRequest("No data found!");
            else
                return Ok("Admin Users Directory Access Updated Successfully");
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString(), cancellationToken).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }
        //[HttpPut("UpdateAdminDirectory")]
        //public async Task<IActionResult> UpdateAdminDirectoryAccess(int id, AdminDirectoryAccessDto admdir, CancellationToken cancellationToken)
        //{
        //    int result = await _adminDirectoryAccessService.UpdateAdminDirectoryAccess(id, admdir, cancellationToken).ConfigureAwait(false);
        //    if (result == 0)
        //        return BadRequest();
        //    else
        //        return Ok();
        //}

        //[HttpPut("DeleteAdminDirectory")]
        //public async Task<IActionResult> DeleteAdminDirectoryAccess(int id, string username, CancellationToken cancellationToken)
        //{
        //    int result = await _adminDirectoryAccessService.DeleteAdminDirectoryAccess(id, username, cancellationToken).ConfigureAwait(false);
        //    if (result == 0)
        //        return BadRequest();
        //    else
        //        return Ok();
        //}


    }
}
