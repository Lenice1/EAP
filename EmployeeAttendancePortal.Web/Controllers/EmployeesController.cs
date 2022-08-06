using AutoMapper;
using InventoryManagement.Web.Constants;
using InventoryManagement.Web.Contracts;
using InventoryManagement.Web.Data;
using InventoryManagement.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly UserManager<Employee> userManager;
        private readonly IMapper mapper;
        private readonly IOrderAllocationRepository orderAllocationRepository;
        private readonly IProductRepository productRepository;

        public EmployeesController(UserManager<Employee> userManager,
            IMapper mapper, IOrderAllocationRepository orderAllocationRepository, 
            IProductRepository productRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.orderAllocationRepository = orderAllocationRepository;
            this.productRepository = productRepository;
        }
        // GET: EmployeesController
        // Display a List of Employees in the Database
        public async Task<IActionResult> Index()
        {
            var employees = await userManager.GetUsersInRoleAsync(Roles.User);
            var model = mapper.Map<List<EmployeeListVM>>(employees);
            return View(model);
        }

        // GET: EmployeesController/ViewAllocations/employee Id
        public async Task<ActionResult> ViewAllocations(string id)
        {
            var model = await orderAllocationRepository.GetEmployeeAllocations(id);
            return View(model);
        }

        

        // GET: EmployeesController/EditAllocation/5
        public async Task <ActionResult> EditAllocation(int id)
        {
            var model = await orderAllocationRepository.GetEmployeeAllocation(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: EmployeesController/EditAllocation/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAllocation(int id, OrderAllocationEditVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   if(await orderAllocationRepository.UpdateEmployeeAllocation(model))
                    {
                        return RedirectToAction(nameof(ViewAllocations), new { id = model.EmployeeId });

                    }
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, " An Error Has Occured. Please Try Again Later.");
            }
             model.Employee = mapper.Map<EmployeeListVM>(await userManager.FindByIdAsync(model.EmployeeId));
            model.Product = mapper.Map<ProductVM>(await productRepository.GetAsync(model.ProductId));

            return View(model);

        }

    }
}
