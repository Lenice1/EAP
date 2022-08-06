using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeAttendancePortal.Web.Data;
using AutoMapper;
using EmployeeAttendancePortal.Web.Models;
using EmployeeAttendancePortal.Web.Contracts;
using Microsoft.AspNetCore.Authorization;
using EmployeeAttendancePortal.Web.Constants;

namespace EmployeeAttendancePortal.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class ELCriteriaController : Controller
    {
       // private readonly ApplicationDbContext _context; IF ERRORS OCCURED REMOVE COMMENT

        private readonly IELCriteriaRepository eLCriteriaRepository;
        private readonly IMapper mapper;
        private readonly IOrderAllocationRepository orderAllocationRepository;

        public ELCriteriaController(IELCriteriaRepository eLCriteriaRepository
            , IMapper mapper
            , IOrderAllocationRepository orderAllocationRepository)
        {
            this.eLCriteriaRepository = eLCriteriaRepository;
            this.mapper = mapper;
            this.orderAllocationRepository = orderAllocationRepository;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var eLCriterias = mapper.Map<List<ELCriteriaVM>>(await eLCriteriaRepository.GetAllAsync());
            return View(eLCriterias);

        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            var eLCriteria = await eLCriteriaRepository.GetAsync(id);
            if (eLCriteria == null)
            {
                return NotFound();
            }

            var eLCriteriaVM = mapper.Map<ELCriteriaVM>(eLCriteria);

            return View(eLCriteriaVM);
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
        public async Task<IActionResult> Create(ELCriteriaVM eLCriteriaVM)
        {
            if (ModelState.IsValid)
            {
                var eLCriteria = mapper.Map<ELCriteria>(eLCriteriaVM);
               await eLCriteriaRepository.AddAsync(eLCriteria);
                return RedirectToAction(nameof(Index));
            }
            return View(eLCriteriaVM);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var eLCriteria = await eLCriteriaRepository.GetAsync(id);
            if (eLCriteria == null)
            {
                return NotFound();
            }

            var eLCriteriaVM = mapper.Map<ELCriteriaVM>(eLCriteria);
           
            return View(eLCriteriaVM);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ELCriteriaVM eLCriteriaVM)
        {
            if (id != eLCriteriaVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = mapper.Map<ELCriteria>(eLCriteriaVM);
                   await eLCriteriaRepository.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await eLCriteriaRepository.Exists(eLCriteriaVM.Id))
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
            return View(eLCriteriaVM);
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
            await eLCriteriaRepository.DeleteAsync(id);
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
