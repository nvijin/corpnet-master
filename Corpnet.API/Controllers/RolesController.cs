using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Corpnet.Data.Interfaces;
using Corpnet.Data.Model;
using Corpnet.Services.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Corpnet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoles _repo;
        private readonly IErrorlogService _errorlogService;

        public RolesController(IRoles repo, IErrorlogService errorlogService)
        {
            this._repo = repo;
            this._errorlogService = errorlogService;
        }

        // GET: api/<RolesController>
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetRoles() => Ok(await _repo.GetRolesAsync().ConfigureAwait(false));


        //// GET api/<RolesController>/5
        //[HttpGet("{id}")]
        //public string GetRoles(int id)
        //{
        //    return "value";
        //}

        //// POST api/<RolesController>
        [HttpPost("{rolename}")]
        public async Task<IActionResult> AddRole(Roles role, CancellationToken cancellationToken)
        {
            try
            {
                //Roles role = new Roles()
                //{
                //    RoleName = rolename,
                //    CreatedBy = "Basha",
                //    ModifiedBy = "Basha",
                //    CreatedDate = DateTime.Now,
                //    ModifiedDate = DateTime.Now
                //};

                await _repo.AddRole(role, cancellationToken);

                return Ok("Role added successfully");
            }
            catch (Exception ex)
            {
               await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString()).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }

        //// PUT api/<RolesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<RolesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
