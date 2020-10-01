using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Corpnet.Data.Interfaces;
using Corpnet.Entities;
using Corpnet.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Corpnet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IFavouriteService _service;
        private readonly IErrorlogService _errorlogService;

        public FavouriteController(IMapper mapper, IFavouriteService service,IErrorlogService errorlogService)
        {
            this.mapper = mapper;
            this._service = service;
            this._errorlogService = errorlogService;
        }

        [HttpGet("{LDAPUser_id}")]
        public async Task<IActionResult> GetGenericPage(string LDAPUser_id)
        {
            try
            {
                string dirJson = await _service.GetFavourite(LDAPUser_id).ConfigureAwait(false);
                return Ok(dirJson);
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString()).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddToFavourite(FavouriteDto favDto, CancellationToken cancellationToken)
        {
            try
            {
                int result = await _service.AddtoFavourite(favDto, cancellationToken).ConfigureAwait(false);
                if (result == 0)
                    return BadRequest();
                else
                    return Ok("Addded To Favourite");
            }
            catch (Exception ex)
            {
                await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString()).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }

    }
}
