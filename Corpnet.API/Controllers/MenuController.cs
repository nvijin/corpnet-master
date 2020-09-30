using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Corpnet.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Http;

namespace Corpnet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly ILogger _logger;

        public MenuController(IMenuService menuService, ILogger<DirectoryController> logger)
        {
            this._menuService = menuService;
            this._logger = logger;
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
                _logger.LogError($"Something went wrong: {ex }");
                return StatusCode(500, ex.InnerException);
            }
        }
    }
}
