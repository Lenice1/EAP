namespace EmployeeAttendancePortal.Web.Data
{
    public class OrderAllocation :BaseEntity
    {
        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }
        

        public Product Product { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }

        public int Quantity_Requested { get; set; }
        public DateTime DateRequested { get; set; }

        public string? OrderStatus { get; set; }

        public string? Department { get; set; }
        public int Period { get; set; }

    }
}
