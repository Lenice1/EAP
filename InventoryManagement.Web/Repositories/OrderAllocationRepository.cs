using AutoMapper;
using EmployeeAttendancePortal.Web.Constants;
using EmployeeAttendancePortal.Web.Contracts;
using EmployeeAttendancePortal.Web.Data;
using EmployeeAttendancePortal.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAttendancePortal.Web.Repositories
{
    public class OrderAllocationRepository : GenericRepository<OrderAllocation>, IOrderAllocationRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Employee> userManager;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public OrderAllocationRepository(ApplicationDbContext context, 
            UserManager<Employee> userManager, IProductRepository productRepository,
            IMapper mapper) : base(context)
        {
            this.context = context;
            this.userManager = userManager;
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<bool> AllocationExists(string employeeId, int productId, int period)
        {
            return await context.OrderAllocations.AnyAsync(q => q.EmployeeId == employeeId
                                                                 && q.ProductId == productId
                                                                 && q.Period == period);
        }
        public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string employeeId)
        {
            var allocations = await context.OrderAllocations
                .Include(q => q.Product)
                .Where(q => q.EmployeeId == employeeId).ToListAsync();
            var employee = await userManager.FindByIdAsync(employeeId);

            var employeeAllocationModel = mapper.Map<EmployeeAllocationVM>(employee);
            employeeAllocationModel.OrderAllocations = mapper.Map<List<OrderAllocationVM>>(allocations);


                return employeeAllocationModel;
        }

        public async Task<OrderAllocationEditVM> GetEmployeeAllocation(int id)
        {
            var allocation = await context.OrderAllocations
                .Include(q => q.Product)
                .FirstOrDefaultAsync(q => q.Id == id);
            if(allocation == null)
            {
                return null;
            }
            var employee = await userManager.FindByIdAsync(allocation.EmployeeId);

            var model= mapper.Map<OrderAllocationEditVM>(allocation);
            model.Employee = mapper.Map<EmployeeListVM>(await userManager.FindByIdAsync (allocation.EmployeeId));

            return model;
        }
        public async Task OrderAllocation(int productId)
        {
            var employees = await userManager.GetUsersInRoleAsync(Roles.User);
            var period = DateTime.Now.Year;
            var product = await productRepository.GetAsync(productId);
            var allocations = new List<OrderAllocation>();
            foreach (var employee in employees)
            {
                if (await AllocationExists(employee.Id, productId, period))
                    continue;
                 allocations.Add ( new OrderAllocation
                {
                    EmployeeId = employee.Id,
                    ProductId = productId,
                    Period = period,
                    ProductName = product.ProductName,
                    ProductQuantity = product.ProductQuantity,
                    Department = employee.EmployeeDepartment
                    
                });
               
            }
            await AddRangeAsync(allocations);
        }

        public async Task<bool> UpdateEmployeeAllocation(OrderAllocationEditVM model)
        {
            var orderAllocation = await GetAsync(model.Id);
            if (orderAllocation == null)
            {
                return false;
            }
            //EDIT FIELDS FOR ALLOCATION HERE
            orderAllocation.ProductName = model.ProductName;
            orderAllocation.ProductQuantity = model.Quantity_Requested;

            await UpdateAsync(orderAllocation);
            return true;
        }

        public async Task<OrderAllocation?> GetEmployeeAllocation(string employeeId, int productId)
        {
           return await context.OrderAllocations.FirstOrDefaultAsync(q => q.EmployeeId == employeeId && q.ProductId == productId);
        }
    }
}
