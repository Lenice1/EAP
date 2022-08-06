using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeAttendancePortal.Web.Data;

namespace EmployeeAttendancePortal.Web.Controllers
{
    public class OrderAllocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderAllocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderAllocations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrderAllocations.Include(o => o.Employee).Include(o => o.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrderAllocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderAllocations == null)
            {
                return NotFound();
            }

            var orderAllocation = await _context.OrderAllocations
                .Include(o => o.Employee)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderAllocation == null)
            {
                return NotFound();
            }

            return View(orderAllocation);
        }

        // GET: OrderAllocations/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: OrderAllocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,Firstname,Lastname,ProductId,ProductName,ProductQuantity,Quantity_Requested,DateRequested,OrderStatus,ItemRequestId,Department,Period,Id,DateCreated,DateModified")] OrderAllocation orderAllocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderAllocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Id", orderAllocation.EmployeeId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", orderAllocation.ProductId);
            return View(orderAllocation);
        }

        // GET: OrderAllocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderAllocations == null)
            {
                return NotFound();
            }

            var orderAllocation = await _context.OrderAllocations.FindAsync(id);
            if (orderAllocation == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Id", orderAllocation.EmployeeId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", orderAllocation.ProductId);
            return View(orderAllocation);
        }

        // POST: OrderAllocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,Firstname,Lastname,ProductId,ProductName,ProductQuantity,Quantity_Requested,DateRequested,OrderStatus,ItemRequestId,Department,Period,Id,DateCreated,DateModified")] OrderAllocation orderAllocation)
        {
            if (id != orderAllocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderAllocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderAllocationExists(orderAllocation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Id", orderAllocation.EmployeeId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", orderAllocation.ProductId);
            return View(orderAllocation);
        }

        // GET: OrderAllocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderAllocations == null)
            {
                return NotFound();
            }

            var orderAllocation = await _context.OrderAllocations
                .Include(o => o.Employee)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderAllocation == null)
            {
                return NotFound();
            }

            return View(orderAllocation);
        }

        // POST: OrderAllocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderAllocations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OrderAllocations'  is null.");
            }
            var orderAllocation = await _context.OrderAllocations.FindAsync(id);
            if (orderAllocation != null)
            {
                _context.OrderAllocations.Remove(orderAllocation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderAllocationExists(int id)
        {
          return (_context.OrderAllocations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
