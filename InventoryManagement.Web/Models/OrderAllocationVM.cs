
using System.ComponentModel.DataAnnotations;

namespace EmployeeAttendancePortal.Web.Models
{
    public class OrderAllocationVM
    {
        [Required]
        public int Id { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Quantity Requested")]
        public int Quantity_Requested { get; set; }
        [Display(Name = "Date Requested")]
        public DateTime DateRequested { get; set; }
        public ProductVM? Product { get; set; }



    }
}