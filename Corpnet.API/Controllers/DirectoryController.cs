using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Corpnet.Data.Interfaces;
using System.Collections.Generic;
using Corpnet.Services.Interfaces;
using Corpnet.Entities;
using System.Threading;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.Extensions;

namespace Corpnet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectoryController : ControllerBase
    {
        private readonly IDirectoryService _dirService;
        private readonly IErrorlogService _errorlogService;
       // private readonly ILogger _logger;

        public DirectoryController(IDirectoryService dirService, IErrorlogService errorlogService)
        {
            this._dirService = dirService;
            this._errorlogService = errorlogService;
            //this._logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetDirDoc(int id, string username)
        {
            try
            {
                string dirJson = await _dirService.GetDirectoryAsync(id, username).ConfigureAwait(false);
                return Ok(dirJson);
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString());
                return StatusCode(500, ex.InnerException);
            }
        }

        [HttpGet("GetDirListAdmin")]
        public async Task<IActionResult> GetDirListMenuBar()
        {
            try
            {
                string dirJson = await _dirService.GetDirListMenuBar().ConfigureAwait(false);
                return Ok(dirJson);
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString());
                return StatusCode(500, ex.InnerException);
            }
        }


        [HttpGet("GetDirById")]
        public async Task<IActionResult> GetDirById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var model = await _dirService.GetDirById(id, cancellationToken).ConfigureAwait(false);
                return Ok(model);
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString()).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddDirectory(DirectoryDto dir, CancellationToken cancellationToken)
        {
            try
            {
                int result = await _dirService.AddDirectory(dir, cancellationToken).ConfigureAwait(false);
                if (result == 0)
                    return BadRequest("No data found!");
                else
                    return Ok("Category added successfully");
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString()).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateDirectory(int id, DirectoryDto dir, CancellationToken cancellationToken)
        {
            try
            {
                int result = await _dirService.UpdateDirectory(id, dir, cancellationToken).ConfigureAwait(false);
                if (result == 0)
                    return BadRequest();
                else
                    return Ok("Category Updated Successfully");
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString()).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteDirectory(int id, string username, CancellationToken cancellationToken)
        {
            try
            {
                int result = await _dirService.DeleteDirectory(id, username, cancellationToken).ConfigureAwait(false);
                if (result == 0)
                    return BadRequest("Category not found!!!");
                else
                    return Ok("Category Deleted Successfully");
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString()).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }

        [HttpPut("UpdateNav")]
        public async Task<IActionResult> UpdateNavigation(IEnumerable<DirectoryNavDto> dirList, CancellationToken cancellationToken)
        {
            try
            {
            int result = await _dirService.UpdateNavigation(dirList, cancellationToken).ConfigureAwait(false);

            if (result == 0)
                return BadRequest("Error while updating!!!");
            else
                return Ok("Navigation Update Successfully");
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString()).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }


        [HttpPut("ShowHide")]
        public async Task<IActionResult> ShowHideDirectory(int id, string ModifiedBy, CancellationToken cancellationToken)
        {
            try
            {
                int result = await _dirService.ShowHideDirectory(id, ModifiedBy, cancellationToken).ConfigureAwait(false);
                if (result == 0)
                    return BadRequest("No data found!");
                else
                    return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString()).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }


    }
}
