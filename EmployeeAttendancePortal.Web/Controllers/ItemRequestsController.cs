using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Web.Data;
using InventoryManagement.Web.Models;
using AutoMapper;
using InventoryManagement.Web.Contracts;
using Microsoft.AspNetCore.Authorization;
using InventoryManagement.Web.Constants;

namespace InventoryManagement.Web.Controllers
{
    [Authorize]
    public class ItemRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IItemRequestRepository itemRequestRepository;

        public ItemRequestsController(ApplicationDbContext context, IItemRequestRepository itemRequestRepository )
        {
            _context = context;
            this.itemRequestRepository = itemRequestRepository;
        }
       
        [Authorize(Roles = Roles.Administrator)]
        // GET: ItemRequests
        public async Task<IActionResult> Index()
        {
            var model = await itemRequestRepository.GetAdminItemRequestList();
            return View(model);
        }
        public async Task<ActionResult> MyRequest()
        {
            var model = await itemRequestRepository.GetMyItemRequestDetails();
            return View(model);
        }

        // GET: ItemRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var model = await itemRequestRepository.GetItemRequestAsync(id);
            if(model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveRequest(int id, bool approved)
        {
            try
            {
                await itemRequestRepository.ChangeApprovalStatus(id, approved);

            }
            catch (Exception ex)
            {

                throw;
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                await itemRequestRepository.CancelItemRequest(id);
            }
            catch (Exception ex)
            {

                throw;
            }
            return RedirectToAction(nameof(MyRequest));
        }
        // GET: ItemRequests/Create
        public IActionResult Create()
        {
            var model = new ItemRequestCreateVM
            {
               // DateRequested = DateTime.Now,
                Products = new SelectList(_context.Products, "Id", "ProductName")            
            };
            return View(model);
        }

        // POST: ItemRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemRequestCreateVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await itemRequestRepository.CreateItemRequest(model);
                    return RedirectToAction(nameof(MyRequest));
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, "An Error Has Occured. Please Try Again Later");
            }
            
            model.Products = new SelectList(_context.Products, "Id", "ProductName", model.ProductId);
            return View(model);
        }

        // GET: ItemRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ItemRequests == null)
            {
                return NotFound();
            }

            var itemRequest = await _context.ItemRequests.FindAsync(id);
            if (itemRequest == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", itemRequest.ProductId);
            return View(itemRequest);
        }

        // POST: ItemRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Quantity_Requested,DateRequested,RequstComments,Approved,Cancelled,RequestingEmployeeId,Id,DateCreated,DateModified")] ItemRequest itemRequest)
        {
            if (id != itemRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemRequestExists(itemRequest.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", itemRequest.ProductId);
            return View(itemRequest);
        }

        // GET: ItemRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ItemRequests == null)
            {
                return NotFound();
            }

            var itemRequest = await _context.ItemRequests
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemRequest == null)
            {
                return NotFound();
            }

            return View(itemRequest);
        }

        // POST: ItemRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ItemRequests == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ItemRequests'  is null.");
            }
            var itemRequest = await _context.ItemRequests.FindAsync(id);
            if (itemRequest != null)
            {
                _context.ItemRequests.Remove(itemRequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemRequestExists(int id)
        {
          return (_context.ItemRequests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
