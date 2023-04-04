using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SportStoreFreeman.Data
{
    public class ApiIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public ApiIdentityDbContext(DbContextOptions<ApiIdentityDbContext> options) : base(options) { }
    }
}
