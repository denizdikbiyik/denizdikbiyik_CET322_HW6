using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using denizdikbiyik_CET322_HW5.Data;
using denizdikbiyik_CET322_HW5.Models;
using denizdikbiyik_CET322_HW5.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace denizdikbiyik_CET322_HW5.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<CetUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(ApplicationDbContext context, UserManager<CetUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ActionResult> Index()
        {

            var userList = _context.Users.ToList();

            List<UserModel> userModelList = new List<UserModel>();

            foreach (var item in userList)
            {
                bool isadmin = await _userManager.IsInRoleAsync(item, "admin");
                bool isdepartmentadmin = await _userManager.IsInRoleAsync(item, "departmentadmin");
                var user = new UserModel
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    FullName = item.FirstName + " " + item.LastName,
                    IsAdmin = isadmin,
                    IsDepartmentAdmin = isdepartmentadmin,
                };
                userModelList.Add(user);
            }
            return View(userModelList);
        }

        public async Task<ActionResult> MakeAdmin(string id)
        {
            if (!(await _roleManager.RoleExistsAsync("admin")))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "admin" });
            }
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.AddToRoleAsync(user, "admin");
            return RedirectToAction("index");
        }

        public async Task<ActionResult> MakeDepartmentAdmin(string id)
        {
            if (!(await _roleManager.RoleExistsAsync("departmentadmin")))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "departmentadmin" });
            }
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.AddToRoleAsync(user, "departmentadmin");
            return RedirectToAction("index");
        }

        public async Task<ActionResult> RemoveAdmin(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.RemoveFromRoleAsync(user, "admin");
            return RedirectToAction("index");
        }

        public async Task<ActionResult> RemoveDepartmentAdmin(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.RemoveFromRoleAsync(user, "departmentadmin");
            return RedirectToAction("index");
        }      

    }
}