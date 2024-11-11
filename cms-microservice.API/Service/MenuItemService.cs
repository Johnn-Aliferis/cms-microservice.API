using cms_microservice.API.Data;
using cms_microservice.API.Repository;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace cms_microservice.API.Service
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IDistributedCache _cache;
        private const string CacheKey = "menuItems";

        //DI
        public MenuItemService(IMenuItemRepository menuItemRepository, IDistributedCache cache)
        {
            _menuItemRepository = menuItemRepository;
            _cache = cache;
        }

        public async Task<MenuItem?> GetMenuItemAsync()
        {
            var cachedMenuItem = await _cache.GetStringAsync(CacheKey);
            if (!string.IsNullOrEmpty(cachedMenuItem))
            {
                return JsonSerializer.Deserialize<MenuItem>(cachedMenuItem);
            }

            var menuItem = await _menuItemRepository.GetMenuItemAsync();
            if (menuItem != null)
            {
                var serializedMenuItem = JsonSerializer.Serialize(menuItem);
                await _cache.SetStringAsync(CacheKey, serializedMenuItem);
            }

            return menuItem;
        }

        public async Task UpdateMenuItemAsync(MenuItem menuItem)
        {
            MenuItem? existingMenuItem = await _menuItemRepository.GetMenuItemAsync();

            if (existingMenuItem == null)
            {
                var menuItemToBePersisted = new MenuItem
                {
                    Data = menuItem.Data,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                await _menuItemRepository.SaveMenuItem(menuItemToBePersisted);
            }
            else
            {
                existingMenuItem.UpdatedAt = DateTime.UtcNow;
                existingMenuItem.Data = menuItem.Data;
                await _menuItemRepository.UpdateMenuItemAsync(existingMenuItem);
            }

            var serializedMenuItem = JsonSerializer.Serialize(existingMenuItem);
            await _cache.SetStringAsync(CacheKey, serializedMenuItem);
        }
    }
}
