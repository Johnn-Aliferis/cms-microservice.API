using cms_microservice.API.Data;
using cms_microservice.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace cms_microservice.API
{
    [Route("api")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        //DI
        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        // GET api/menu-item
        [HttpGet("get-menu-items")]
        public async Task<ActionResult<MenuItem?>> GetMenuItem()
        {
            try
            {
                var menuItem = await _menuItemService.GetMenuItemAsync();
                if (menuItem == null)
                {
                    return NotFound();
                }

                return Ok(menuItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
