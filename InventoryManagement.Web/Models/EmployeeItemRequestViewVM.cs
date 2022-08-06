namespace EmployeeAttendancePortal.Web.Models
{
    public class EmployeeItemRequestViewVM
    {
        public EmployeeItemRequestViewVM(List<OrderAllocationVM> orderAllocations, List<ItemRequestVM> itemRequests)
        {
            OrderAllocations = orderAllocations;
            ItemRequests = itemRequests;
        }

        public List<OrderAllocationVM> OrderAllocations { get; set; }
        public List<ItemRequestVM> ItemRequests { get; set; }
        
    }
}
