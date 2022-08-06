using EmployeeAttendancePortal.Web.Data;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EmployeeAttendancePortal.Web.Models
{
    public class ProductVM : BaseEntity
    {
        [Display(Name = "Attendance")]
        [Required]
        public string ProductName { get; set; }
        public string Description { get; set; }
        [Display(Name = "Re-Order Level")]
        [Range(1, 30, ErrorMessage = "Please Enter A Valid Number")]
        public int ReOrderLevel { get; set; }
        [Display(Name = "Product Quantity")]
        [Range(1,1000, ErrorMessage ="Please Enter A Valid Number")]
        public int ProductQuantity { get; set; }

    }
}
