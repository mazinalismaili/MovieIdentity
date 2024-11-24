using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieIdentity.Models;
using MovieIdentity.Models.ViewModel;
using System.Data;

namespace MovieIdentity.Controllers
{
    //[Authorize(Roles = "Manager")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager,UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Index()
        {
            // declare a list of type UserRolesViewModel to store all users and their own role:
            List<UserRolesViewModel> UsersInfo = new List<UserRolesViewModel>();

            // get all users:
            var users = _userManager.Users.ToList();

            // loop through each user and get his roles:
            foreach(var user in users)
            {
                var useRroles = await _userManager.GetRolesAsync(user);
                // insert it the user and his role in UsersInfo:
                var userInfo = new UserRolesViewModel()
                {
                    User = user,
                    Roles = useRroles
                };
                // add it to the list:
                UsersInfo.Add(userInfo);
            }

            // get all users:
            var userModel = users.Select(user => new UserModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
            }).ToList();

            // get all roles:
            var roles = await _roleManager.Roles.ToListAsync();

            var roleModel = roles.Select(role => new Role
            {
                Id = role.Id,
                Name = role.Name,
            }).ToList();

            AdminViewModel AdminViewModel = new AdminViewModel()
            {
                UserRolesViewModels = UsersInfo,
                Users = userModel,
                Roles = roleModel
            };

            return View(AdminViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRoleToUser(string userid, string rolename)
        {
            var employee = await _userManager.FindByIdAsync(userid);
            if (ModelState.IsValid && employee != null)
            {
                IdentityResult result = await _userManager.AddToRoleAsync(employee, rolename);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRoleFromUser(string id, string name)
        {
            var employee = await _userManager.FindByIdAsync(id);
            if (ModelState.IsValid && employee != null)
            {
                IdentityResult result = await _userManager.RemoveFromRoleAsync(employee, name);
            }

            return RedirectToAction("Index");
        }


    }
}
