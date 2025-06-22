using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Product.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastrucre.Data.Config
{
    public class IdentitySeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            if (!await userManager.Users.AnyAsync())
            {
                var user = new AppUser
                {
                    DisplayName = "DuyBinh",
                    Email = "duybinh@gmail.com",
                    UserName = "duybinh@gmail.com"
                };

                // Tạo user trước
                var result = await userManager.CreateAsync(user, "hd8b122003");

                if (result.Succeeded)
                {
                    var address = new Address
                    {
                        FirstName = "Ha",
                        LastName = "DuyBinh",
                        City = "VietNam",
                        State = "NinhHoa",
                        Street = "Street",
                        ZipCode = "797979",
                        AppUserId = user.Id
                    };

                    context.Addresses.Add(address);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
