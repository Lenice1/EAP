using EmployeeAttendancePortal.Web.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAttendancePortal.Web.Models
{
    public class ItemRequestVM : ItemRequestCreateVM
    {
        public int Id { get; set; }

        [Display(Name = "Item Name")]
        public int ProductQuantity { get; set; }

        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }
        public string? RequestingEmployeeId { get; set; }
       public  EmployeeListVM Employee { get; set; }
    }
}
