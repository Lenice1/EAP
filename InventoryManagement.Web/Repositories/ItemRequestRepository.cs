using AutoMapper;
using EmployeeAttendancePortal.Web.Contracts;
using EmployeeAttendancePortal.Web.Data;
using EmployeeAttendancePortal.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAttendancePortal.Web.Repositories
{
    public class ItemRequestRepository : GenericRepository<ItemRequest>, IItemRequestRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IOrderAllocationRepository orderAllocationRepository;
        private readonly UserManager<Employee> userManager;

        public ItemRequestRepository(ApplicationDbContext context, 
            IMapper mapper, 
            IHttpContextAccessor httpContextAccessor,
            IOrderAllocationRepository orderAllocationRepository,
            UserManager<Employee> userManager ) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.orderAllocationRepository = orderAllocationRepository;
            this.userManager = userManager;
        }

        public async Task ChangeApprovalStatus(int itemRequestId, bool approved)
        {
            var itemRequest = await GetAsync(itemRequestId);
            itemRequest.Approved = approved;
            if (approved)
            {
                //Changes Need to Be made here to edit allocaiton
                var allocation = await orderAllocationRepository.GetEmployeeAllocation(itemRequest.RequestingEmployeeId, itemRequest.ProductId);
                int quantityRequested = (int)(itemRequest.Quantity_Requested - itemRequest.ProductQuantity);
                await orderAllocationRepository.UpdateAsync(allocation);
            }
            await UpdateAsync(itemRequest);
        }   

        public async Task CreateItemRequest(ItemRequestCreateVM model)
        {
            var user = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);

            var itemRequest = mapper.Map<ItemRequest>(model);
            itemRequest.DateRequested = DateTime.Now;
            itemRequest.RequestingEmployeeId = user.Id;
            await AddAsync(itemRequest);

        }

        public async Task<AdminItemRequestViewVM> GetAdminItemRequestList()
        {
            var itemRequests = await context.ItemRequests.Include(q=> q.Product).ToListAsync();
            var model = new AdminItemRequestViewVM
            {
                TotalRequests = itemRequests.Count,
                ApprovedRequests = itemRequests.Count(q => q.Approved == true),
                PendingRequests = itemRequests.Count(q => q.Approved == null),
                DeniedRequests = itemRequests.Count(q => q.Approved == false),
                ItemRequests = mapper.Map<List<ItemRequestVM>>(itemRequests),

            };
            foreach (var itemRequest in model.ItemRequests)
            {
                itemRequest.Employee  = mapper.Map<EmployeeListVM> (await userManager.FindByIdAsync(itemRequest.RequestingEmployeeId));
            }
            return model;
        }

        public async Task<List<ItemRequest>> GetAllAsync(string employeeId)
        {
            return await context.ItemRequests.Where(q => q.RequestingEmployeeId == employeeId).ToListAsync();
        }

       
        public async Task<EmployeeItemRequestViewVM> GetMyItemRequestDetails()
        {
            var user = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);
            var allocations = (await orderAllocationRepository.GetEmployeeAllocations(user.Id)).OrderAllocations;
            var requests = mapper.Map<List<ItemRequestVM>> (await GetAllAsync(user.Id));

            var model = new EmployeeItemRequestViewVM(allocations, requests);

            return model;
        }

     

        public async Task<ItemRequestVM?>GetItemRequestAsync(int? id)
        {
            var itemRequest = await context.ItemRequests
               .Include(q => q.Product)
               .FirstOrDefaultAsync(q => q.Id == id);

            if (itemRequest == null)
            {
                return null;
            }

            var model = mapper.Map<ItemRequestVM>(itemRequest);
            model.Employee = mapper.Map<EmployeeListVM>(await userManager.FindByIdAsync(itemRequest?.RequestingEmployeeId));
            return model;
        }

        public async Task CancelItemRequest(int itemRequestId)
        {
            var itemRequest = await GetAsync(itemRequestId);
            itemRequest.Cancelled = true;
            await UpdateAsync(itemRequest);
        }
    }
}
