using Microsoft.AspNetCore.Identity;

namespace SunsetsIO.Models
{
    public class User:IdentityUser
    {
        public List<Rating> Ratings { get; set; }
    }
}
