using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Auth_Service.Model;
using Microsoft.EntityFrameworkCore;

namespace Auth_Service.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { 
            
        }
    }
}
