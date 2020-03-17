using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebVuiVN.Data.Entities;
using WebVuiVN.Data.Enums;

namespace WebVuiVN.Data.EF
{
    public class SeedData
    {
        private readonly WebVuiVNDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        public SeedData(WebVuiVNDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task Initialize()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Top manager"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Staff",
                    NormalizedName = "Staff",
                    Description = "Staff"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Customer",
                    NormalizedName = "Customer",
                    Description = "Customer"
                });
            }
            if (_context.Postcategorys.Count() == 0)
            {
                List<PostCategory> listPostCategory = new List<PostCategory>()
                {
                    new PostCategory() { Name="Men shirt",Description="co chang trai",Image="ấdsf",Status=Status.Active,
                        Posts = new List<Post>()
                        {
                            new Post(){Name = "Post 1",Text="xin chao moi nguoi",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",Status = Status.Active},
                            new Post(){Name = "Post 2",Text="xin chao moi nguoi",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",Status = Status.Active },
                            new Post(){Name = "Post 3",Text="xin chao moi nguoi",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",Status = Status.Active},
                            new Post(){Name = "Post 4",Text="xin chao moi nguoi",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",Status = Status.Active},
                            new Post(){Name = "Post 5",Text="xin chao moi nguoi",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",Status = Status.Active},
                        }
                    }
                };
                _context.Postcategorys.AddRange(listPostCategory);
            }
            await _context.SaveChangesAsync();

            if (!_userManager.Users.Any())
            {   
                _ = await _userManager.CreateAsync(new AppUser("admin1", "admin1", "lethanh@gmail.com", "03622", "anh", Status.Active)
                {
                    //  Balance = 0,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                }, "123654A");
                var user = await _userManager.FindByNameAsync("admin1");
                await _userManager.AddToRoleAsync(user, "Admin");
            }
           
            await _context.SaveChangesAsync();
        }
    }
}
