using cms_microservice.API.Data;

namespace cms_microservice.API.Repository
{
    public interface IMenuItemRepository
    {
        Task<MenuItem?> GetMenuItemAsync();
        Task UpdateMenuItemAsync(MenuItem menuItem);
    }
}
