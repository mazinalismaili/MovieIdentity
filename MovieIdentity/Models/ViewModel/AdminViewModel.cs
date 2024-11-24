using Microsoft.AspNetCore.Identity;
using System.Data;

namespace MovieIdentity.Models.ViewModel
{
    public class AdminViewModel
    {
        public List<UserRolesViewModel> UserRolesViewModels { get; set; }
        public IEnumerable<UserModel> Users { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}
