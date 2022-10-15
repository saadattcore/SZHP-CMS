using Microsoft.AspNet.Identity.EntityFramework;

namespace SZHPCMS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : IdentityUser
    //{
    //}


    //New drived classes 
    public class UserRole : IdentityUserRole<long> { }
    public class UserClaim : IdentityUserClaim<long> { }
    public class UserLogin : IdentityUserLogin<long> { }




    public class Role : IdentityRole<long, UserRole>
    {
        public Role() { }
        public Role(string name) { Name = name; }
    }

    public class ApplicationUser : IdentityUser<long, UserLogin, UserRole, UserClaim>
    {

    }

    public class ApplicationUserStore : UserStore<ApplicationUser, Role, long, UserLogin, UserRole, UserClaim>
    {
        public ApplicationUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class ApplicationRoleStore : RoleStore<Role, long, UserRole>
    {
        public ApplicationRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, long, UserLogin, UserRole, UserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}