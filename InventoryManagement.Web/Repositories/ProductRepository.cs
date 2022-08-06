using EmployeeAttendancePortal.Web.Contracts;
using EmployeeAttendancePortal.Web.Data;

namespace EmployeeAttendancePortal.Web.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
