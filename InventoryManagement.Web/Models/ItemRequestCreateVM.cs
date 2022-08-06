using EmployeeAttendancePortal.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAttendancePortal.Web.Models
{
    public class ItemRequestCreateVM 
    {
        public ProductVM? Product { get; set; }

        [Required]
        [Display(Name = "Late/Early Attendance")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Quantity Requested")]
        [Range(1, 30, ErrorMessage = "Please enter a valid number, greater than 0.")]
        public int Quantity_Requested { get; set; }

        public SelectList? Products { get; set; }
        [Display(Name = "Arrival Date/Time")]
        public DateTime DateRequested { get; set; }
        public DateTime EarlyTime { get; set; }
        [Display(Name = "Attendance Comments")]

        public string? RequstComments { get; set; }
    }
      

}
