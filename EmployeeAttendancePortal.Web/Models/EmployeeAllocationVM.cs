namespace InventoryManagement.Web.Models
{
    public class EmployeeAllocationVM : EmployeeListVM
    {
        public List<OrderAllocationVM> OrderAllocations { get; set; }
       
    }
}
