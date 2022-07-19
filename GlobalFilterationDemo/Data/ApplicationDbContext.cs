using GlobalFilterationDemo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GlobalFilterationDemo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //private readonly IUserProvider userProvider;
        private readonly IHttpContextAccessor accessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor accessor)
            : base(options)
        {
            this.accessor = accessor;
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Employee>(e => {
                e.HasQueryFilter(m => (accessor.HttpContext != null && accessor.HttpContext.User.Identity.IsAuthenticated) || m.Salary <= 50000);
            });
        }

        public virtual DbSet<Employee> Employees { get; set; }
    }
}