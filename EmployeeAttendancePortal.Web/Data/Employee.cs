using Microsoft.AspNetCore.Identity;

namespace InventoryManagement.Web.Data
{
    public class Employee : IdentityUser
    {
       
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? EmployeeDepartment { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined { get; set; }

    }
}
