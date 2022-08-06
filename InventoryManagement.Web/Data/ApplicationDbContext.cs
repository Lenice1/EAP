using EmployeeAttendancePortal.Web.Configurations.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EmployeeAttendancePortal.Web.Models;

namespace EmployeeAttendancePortal.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<Employee>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleSeedConfiguration());
            builder.ApplyConfiguration(new UserSeedConfiguration());
            builder.ApplyConfiguration(new UserRoleSeedConfiguration());

        }

        public DbSet<ELCriteria> Products{ get; set; }
        public DbSet<ItemRequest> ItemRequests{ get; set; }
        public DbSet<OrderAllocation> OrderAllocations{ get; set; }


    }
}