using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieIdentity.Data;

namespace MovieIdentity.Controllers
{
    public class RoleController : Controller
    {
        // GET: RoleController
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return View(roles);
        }

        // GET: RoleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                // Check if Role exist:
                string role_name = collection["name"].ToString();
                var r = await _roleManager.FindByNameAsync(role_name);
                if (r != null)
                {
                    return RedirectToAction("Index");
                }

                // Create a new Role Object:
                IdentityRole role = new IdentityRole(role_name);
                await _roleManager.CreateAsync(role);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: RoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, IFormCollection collection)
        {
            try
            {
                // check if the role exist:
                IdentityRole role = await _roleManager.FindByIdAsync(id);
                if(role != null)
                {
                    string newName = collection["name"].ToString();
                    role.Name = newName;
                    await _roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));

                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: RoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                // check if the role exist:
                IdentityRole role = await _roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    await _roleManager.DeleteAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
