using Microsoft.AspNetCore.Identity;

namespace Character.Infrastructure.Models
{
    public class UserModel : IdentityUser
    {

        public  int LevelUser { get; set; }

    }
}
