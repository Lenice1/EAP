using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeAttendancePortal.Web.Configurations.Entities
{
    public class UserRoleSeedConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "605ae320-30b8-4996-8c99-bf4d3274cf32",
                    UserId = "641441d9-073b-45d3-81d5-7edbcb0faff7"
                },
                 new IdentityUserRole<string>
                 {
                     RoleId = "502aA320-30b8-4996-8A99-bf4d3274af34",
                     UserId = "2b06a079-d2c3-4b00-bb14-148b795cc2f9"
                 }
                );
        }
    }
}
