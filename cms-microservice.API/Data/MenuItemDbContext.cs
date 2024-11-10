using Microsoft.EntityFrameworkCore;

namespace cms_microservice.API.Data
{
    public class MenuItemDbContext : DbContext
    {
        public MenuItemDbContext(DbContextOptions options) : base(options) 
        {
            
        }
        public DbSet<MenuItem> MenuItems { get; set; }
    }

    // If we have a large dataset for example , we might wanna use Reflection to dynamically generate,
    // instead of adding one by one as above.
}
