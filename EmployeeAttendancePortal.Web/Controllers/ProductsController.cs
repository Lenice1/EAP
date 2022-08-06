using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Web.Data;
using AutoMapper;
using InventoryManagement.Web.Models;
using InventoryManagement.Web.Contracts;
using Microsoft.AspNetCore.Authorization;
using InventoryManagement.Web.Constants;

namespace InventoryManagement.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class ProductsController : Controller
    {
       // private readonly ApplicationDbContext _context; IF ERRORS OCCURED REMOVE COMMENT

        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IOrderAllocationRepository orderAllocationRepository;

        public ProductsController(IProductRepository productRepository
            , IMapper mapper
            , IOrderAllocationRepository orderAllocationRepository)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.orderAllocationRepository = orderAllocationRepository;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = mapper.Map<List<ProductVM>>(await productRepository.GetAllAsync());
            return View(products);

        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            var product = await productRepository.GetAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var productVM = mapper.Map<ProductVM>(product);

            return View(productVM);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var product = mapper.Map<Product>(productVM);
               await productRepository.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(productVM);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var product = await productRepository.GetAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var productVM = mapper.Map<ProductVM>(product);
           
            return View(productVM);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductVM productVM)
        {
            if (id != productVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = mapper.Map<Product>(productVM);
                   await productRepository.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await productRepository.Exists(productVM.Id))
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
            return View(productVM);
        }

        // GET: Products/Delete/5
       // public async Task<IActionResult> Delete(int? id)
       // {
       //     if (id == null || _context.Products == null)
        //    {
        //        return NotFound();
        //    }

       //     var product = await _context.Products
         //       .FirstOrDefaultAsync(m => m.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
         //   }

        //    return View(product);
       // }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await productRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllocateOrder(int id)
        {
            await orderAllocationRepository.OrderAllocation(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
