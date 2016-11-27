using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Publications.Models;
using Publications.Models.Entities;
using Publications.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Services
{
    public class AdminService
    {
        private ApplicationDbContext _db;

        public AdminService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<UserListVM>> GetAllUsers(UserManager<ApplicationUser> userManager)
        {
            var users = _db.ApplicationUser.ToList();
            List<UserListVM> userList = new List<UserListVM>();
            
            foreach (var item in users)
            {
                bool isAdmin = await userManager.IsInRoleAsync(item, "Admin");

                userList.Add(new UserListVM
                {
                    Email = item.Email,
                    Id = item.Id,
                    isAdmin = isAdmin
                });
            }

            return userList;
        }

    }
}
