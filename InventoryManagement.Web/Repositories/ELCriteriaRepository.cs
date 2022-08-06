using EmployeeAttendancePortal.Web.Contracts;
using EmployeeAttendancePortal.Web.Data;

namespace EmployeeAttendancePortal.Web.Repositories
{
    public class ELCriteriaRepository : GenericRepository<ELCriteria>, IELCriteriaRepository
    {
        public ELCriteriaRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
