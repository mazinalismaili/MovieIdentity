using Microsoft.AspNetCore.Identity;

namespace MovieIdentity.Models.ViewModel
{
    public class UserRolesViewModel
    {
        public IdentityUser User { get; set; }
        public IList<string>? Roles { get; set; }
    }
}
