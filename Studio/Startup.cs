using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Studio.Models;
using System;
using System.Threading.Tasks;

[assembly: OwinStartupAttribute(typeof(Studio.Startup))]
namespace Studio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //createRolesAndUsers();
        }

        public void createRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "kartuzov@rogers.ca";
                user.Email = "kartuzov@rogers.ca";
                string userPWD = "Canada98";

                var chkUser = userManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Admin");
                }
            }

            if (!roleManager.RoleExists("Teacher"))
            {
                var role = new IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Student"))
            {
                var role = new IdentityRole();
                role.Name = "Student";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "student@gmail.com";
                user.Email = "studnet@gmail.com";

                var chkUser2 = userManager.Create(user, "student18");


                if (chkUser2.Succeeded)
                {
                    var result2 = userManager.AddToRole(user.Id, "Student");
                }
            }
        }


 

    }


}
