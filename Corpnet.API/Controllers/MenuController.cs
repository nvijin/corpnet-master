using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Corpnet.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Corpnet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IErrorlogService _errorlogService;

        public MenuController(IMenuService menuService, IErrorlogService errorlogService)
        {
            this._menuService = menuService;
            this._errorlogService = errorlogService;
        }

        [HttpGet("{menuType}")]
        public async Task<IActionResult> GetMenu(string menuType)
        {
            try
            {

                string MenuJson = await _menuService.GetMainMenuAsync(menuType).ConfigureAwait(false);
                return Ok(MenuJson);
            }
            catch (Exception ex)
            {
               await _errorlogService.InsertError(Request.GetDisplayUrl(), ControllerContext.ActionDescriptor.ActionName.ToString(), ex.Message, ex.ToString()).ConfigureAwait(false);
                return StatusCode(500, ex.InnerException);
            }
        }
    }
}
