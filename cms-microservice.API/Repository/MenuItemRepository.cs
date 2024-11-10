using cms_microservice.API.Data;
using Microsoft.EntityFrameworkCore;

namespace cms_microservice.API.Repository
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly MenuItemDbContext _context;

        public MenuItemRepository(MenuItemDbContext context)
        {
            _context = context;
        }

        public async Task<MenuItem?> GetMenuItemAsync()
        {
            return await _context.MenuItems.FirstOrDefaultAsync();
        }

        public async Task UpdateMenuItemAsync(MenuItem menuItem)
        {
            _context.MenuItems.Update(menuItem);
            await _context.SaveChangesAsync();
        }
    }
}
