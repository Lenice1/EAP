using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAttendancePortal.Web.Data
{
    public class Product:BaseEntity
    {
       
        public string ProductName { get; set; }
        public string Description   { get; set; }
        public int ReOrderLevel { get; set; }
        public int ProductQuantity { get; set; }

    }
}
