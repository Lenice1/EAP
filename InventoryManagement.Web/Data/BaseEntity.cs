using System.ComponentModel.DataAnnotations;

namespace EmployeeAttendancePortal.Web.Data
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
