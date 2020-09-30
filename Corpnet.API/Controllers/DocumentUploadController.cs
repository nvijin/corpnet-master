using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Corpnet.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Corpnet.Data.Model;
using System.Threading;
using Newtonsoft.Json;
using Corpnet.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.Extensions;

namespace Corpnet.API.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("MyPolicy")]
    public class DocumentUploadController : ControllerBase
    {

        private readonly IDocumentUploadService _formService;
        private readonly IHostEnvironment _environment;
        private readonly IErrorlogService _errorlogService;
        IConfiguration Configuration;

        public DocumentUploadController(IHostEnvironment environment, IDocumentUploadService FormService, IConfiguration configuration, IErrorlogService errorlogService)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            this._formService = FormService;
            this._errorlogService = errorlogService;
            Configuration = configuration;
        }

        // POST: api/Image
        [HttpPost]
        //  public async Task Post(IFormFile file,  Int32 ID)
        public async Task<string> UploadDocument(int id, DocumentUp Model, IFormFile files, CancellationToken cancellationToken) // DocumentUpload Model, List<IFormFile> files, CancellationToken cancellationToken, , string Image
        {
            try
            { 
            //File upload not allowed to in root folder
            if (Model.Parent_id == 0)
                return ("Select a sub folder to upload file.");

            int result = 0;
            if (id != 0 || files == null) //Delete // edit
            {
                if (files == null) // update existing record
                {

                    DocumentDto UpdateModel = new DocumentDto()
                    {
                        DocName = Model.DocName,
                        DocDescription = Model.DocDescription,
                        Fk_Directory_id = Model.Parent_id,
                        CreatedBy = Model.CreatedBy
                    };

                    result = await _formService.UpdateDocument(id, UpdateModel, cancellationToken).ConfigureAwait(false);


                    if (result == 1)
                        return "Document Updated";
                    else
                        return "Document Not Found!";
                }
                else
                {
                    result = await _formService.DeleteDocument(id, Model.CreatedBy, cancellationToken).ConfigureAwait(false);
                    if (result == 0)
                        return "Document not found!";
                }
            }

            // 1 . Check/Create Directory & set the File upload path
            var RootDir = Configuration.GetValue<string>("RootDirectory");
            var MaxFileSize = Configuration.GetValue<int>("MaxFileSize");

            string dirJson = await _formService.GetDocumentUpload(Model.Parent_id, RootDir).ConfigureAwait(false);

            string doctype = "";
            long Filesize = 0;
            //var filePath = Path.Combine(_environment.ContentRootPath, dirJson);
            var filePath = _environment.ContentRootPath;

            //2. Upload the files in the set path

            if (files != null)
            {
                Filesize = files.Length;
                FileInfo fi = new FileInfo(files.FileName);
                doctype = fi.Extension;

                var filePaths = new List<string>();
                if (files.Length > 0 && files.Length < MaxFileSize)
                {

                    filePaths.Add(filePath);
                    var fileNameWithPath = string.Concat(filePath, "\\", Model.DocName, doctype);
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await files.CopyToAsync(stream);
                    }

                    Model.DocPath = fileNameWithPath;
                }
            }
            else
            {

                // update the name without uploading
                doctype = doctype.Replace(".", "");
                DocumentDto AddModel = new DocumentDto()
                {
                    //id = Model.id,
                    DocName = Model.DocName,
                    DocDescription = Model.DocDescription,
                    Fk_Directory_id = Model.Parent_id,
                    DocPath = filePath,
                    DocType = doctype,
                    DocSize = 0,
                    CreatedBy = Model.CreatedBy
                };

                result = await _formService.AddDocument(AddModel, cancellationToken).ConfigureAwait(false);
                ////////////////////
                if (result == 1)
                {
                    return ("Document Details Updated");
                }
                else
                {
                    return ("Document size exceeds maximum limit " + MaxFileSize / 1000000 + " MB");
                }
            }

            doctype = doctype.Replace(".", "");

            DocumentDto DocModel = new DocumentDto()
            {
                //id = Model.id,
                DocName = Model.DocName,
                DocDescription = Model.DocDescription,
                Fk_Directory_id = Model.Parent_id,
                DocPath = Model.DocPath,
                DocType = doctype,
                DocSize = (int)Filesize,
                CreatedBy = Model.CreatedBy
            };

            result = await _formService.AddDocument(DocModel, cancellationToken).ConfigureAwait(false);
            return ("Document Uploaded Successfully");
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString(), cancellationToken).ConfigureAwait(false);
                // StatusCode(500, ex.InnerException);
                return (ex.ToString());
            }
        }


        [HttpPut("Delete")]
        public async Task<IActionResult> DeleteDocument(int id, string username, CancellationToken cancellationToken)
        {
            try
            {
                int result = await _formService.DeleteDocument(id, username, cancellationToken).ConfigureAwait(false);
                if (result == 0)
                    return BadRequest("Document not found!!");
                else
                    return Ok("Document Deleted Successfully");
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString(), cancellationToken).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }

        [HttpGet("GetBreadCrumb")]
        public async Task<string> GetBread(int id, CancellationToken cancellationToken) 
        {
            try
            {
                string dirJson = await _formService.GetBread(id).ConfigureAwait(false);
                return (dirJson);
            }
            catch(Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString(), cancellationToken).ConfigureAwait(false);
                // StatusCode(500, ex.InnerException);
                return (ex.ToString());
            }
        }



    }

}