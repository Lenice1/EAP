using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAttendancePortal.Web.Data
{
    public class ItemRequest : BaseEntity
    {

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public int Quantity_Requested { get; set; }
        public DateTime DateRequested { get; set; }
        public string? RequstComments { get; set; }
        public DateTime EarlyTime { get; set; }

        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }
        public string RequestingEmployeeId { get; set; }
    }
}
