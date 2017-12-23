using Microsoft.AspNetCore.Identity;

namespace Jimmy_Waern_Assignment2.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public int Age { get; set; }
    }
}
