using cms_microservice.API.Data;
using cms_microservice.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace cms_microservice.API.Controller
{
    [Route("api")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        //Dependency Injection
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

        [HttpPost("update-menu-items")]
        public async Task<ActionResult> UpdateMenuItems([FromBody] MenuItem menuItem)
        {
            try
            {
                await _menuItemService.UpdateMenuItemAsync(menuItem);
                return Ok("Successfully updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
