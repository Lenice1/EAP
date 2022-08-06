namespace EmployeeAttendancePortal.Web.Models
{
    public class OrderAllocationEditVM : OrderAllocationVM
    {
        public string EmployeeId {  get; set; }
        public int ProductId { get; set; }
        public EmployeeListVM? Employee { get; set; }
    }
}
