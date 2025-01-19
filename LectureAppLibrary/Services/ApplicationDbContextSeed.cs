using LectureAppLibrary.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureAppLibrary.Services
{
    public class ApplicationDbContextSeed
    {
        public static void SeedAdminUser(MyContext context)
        {
            var existingAdmin = context.Admins.FirstOrDefault(a => a.Email == "admin@admin.com");

            if (existingAdmin == null)
            {
                var admin = new Admin
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@admin.com",
                };
                var password = "12345678";
                var hasher = new PasswordHasher<Admin>();
                var hashedPassword = hasher.HashPassword(admin, password);
                admin.Password = hashedPassword;

                context.Admins.Add(admin);
                context.SaveChanges();
            }
        }
    }
}
