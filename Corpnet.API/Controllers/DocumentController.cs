using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Corpnet.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.Extensions;
namespace Corpnet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _docService;
        private readonly IErrorlogService _errorlogService;
        IConfiguration Configuration;
        public DocumentController(IDocumentService docService,IConfiguration configuration, IErrorlogService errorlogService)
        {
            this._docService = docService;
            this.Configuration = configuration;
            this._errorlogService = errorlogService;
        }

        [HttpGet]
        [Route("GetNotification")]
        public async Task<IActionResult> GetNotification()
        {
            try
            {
                string MenuJson = await _docService.GetNotification().ConfigureAwait(false);
                return Ok(MenuJson);
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString(), cancellationToken).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }


        [HttpGet("search/{query}")]
        public async Task<IActionResult> GetGenericPage(string query)
        {
            try
            {
                string dirJson = await _docService.GetSearchResult(query).ConfigureAwait(false);
                return Ok(dirJson);
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString(), cancellationToken).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }


        [HttpGet]
        [Route("GetFiles")]
        public async Task<IActionResult> GetFilesById(int id)
        {
            try
            {
                string ResultJson = await _docService.GetFilesById(id).ConfigureAwait(false);
                return Ok(ResultJson);
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString(), cancellationToken).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }

        [HttpPut("ShowHide")]
        public async Task<IActionResult> ShowHideDocument(int id, string ModifiedBy, CancellationToken cancellationToken)
        {
            try
            {
                int result = await _docService.ShowHideDocument(id, ModifiedBy, cancellationToken).ConfigureAwait(false);
                if (result == 0)
                    return BadRequest("No data found!");
                else
                    return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString(), cancellationToken).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }

        [HttpGet]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile(int id, CancellationToken cancellationToken)
        {
            try
            {
                string filepath = await _docService.GetFileById(id, cancellationToken);
                if (string.IsNullOrWhiteSpace(filepath))
                    return NotFound("File not found!!");

                var RootDir = Configuration.GetValue<string>("RootDirectory");
                string filename = Path.GetFileName(filepath);
                string fullpath = RootDir  + filepath;
                var stream = new FileStream(fullpath, FileMode.Open);
                return File(stream, _docService.GetMime(filename), filename);
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString(), cancellationToken).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }



        [HttpGet]
        [Route("DownloadFile2")]
        public async Task<IActionResult> DownloadFile2(int id, CancellationToken cancellationToken)
        {
            try
            {
                string filepath = await _docService.GetFileById(id, cancellationToken);
                if (string.IsNullOrWhiteSpace(filepath))
                    return NotFound("File not found!!");

                var RootDir = Configuration.GetValue<string>("RootDirectory");
                string filename = Path.GetFileName(filepath);
                string fullpath = RootDir + filepath;

                var stream = new FileStream(fullpath, FileMode.Open);
                if (stream == null)
                    return NotFound("File not found!!");
                //return File(stream, _docService.GetMime(filename), filename);
                //return File(stream, "application/octet-stream"); // returns a FileStreamResult
                return new FileStreamResult(stream, _docService.GetMime(filename));
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString(), cancellationToken).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }

    }
}
