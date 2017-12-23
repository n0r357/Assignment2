using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jimmy_Waern_Assignment2.Models
{
    public class UserContext : IdentityDbContext<User, UserRole, string>
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }
    }
}
