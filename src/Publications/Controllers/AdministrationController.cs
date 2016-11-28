using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Publications.Models;
using Publications.Services;
using Microsoft.AspNetCore.Identity;
using Publications.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Publications.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private AdminService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleMenager;

        public AdministrationController(AdminService service, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleMenager)
        {
            _userManager = userManager;
            _roleMenager = roleMenager;
            _service = service;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _service.GetAllUsers(_userManager);

            return View(users);
        }

        public async Task<IActionResult> AddAdmin(string id)
        {
            var role = await _roleMenager.FindByNameAsync("Admin");
            if (role == null)
            {
                await _roleMenager.CreateAsync(new IdentityRole("Admin"));
            }
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.AddToRoleAsync(user, "Admin");

            return RedirectToAction("Users");
        }

        public async Task<IActionResult> RemoveAdmin(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.RemoveFromRoleAsync(user, "Admin");

            return RedirectToAction("Users");
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);

            return RedirectToAction("Users");
        }
    }
}