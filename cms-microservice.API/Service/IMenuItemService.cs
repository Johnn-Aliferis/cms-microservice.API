using cms_microservice.API.Data;

namespace cms_microservice.API.Service
{
    public interface IMenuItemService
    {
        Task<MenuItem?> GetMenuItemAsync();
        Task UpdateMenuItemAsync(MenuItem menuItem);
    }
}
