using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FYP.DB.Context;
using FYP.DB.DBTables;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.Data.SqlClient;

namespace FYP.Web.Controllers.Partners
{
    [Authorize(Roles = ("Admin,Purchase"))]
    public class VendorsController : Controller
    {
        private readonly FYPContext _context;

        public VendorsController(FYPContext context)
        {
            _context = context;
        }

        // GET: Vendors
        public async Task<IActionResult> Index()
        {
              return _context.Vendors != null ? 
                          View(await _context.Vendors.ToListAsync()) :
                          Problem("Entity set 'FYPContext.Vendors'  is null.");
        }

        // GET: Vendors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vendors == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors
                .FirstOrDefaultAsync(m => m.vendor_id == id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // GET: Vendors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vendors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("vendor_id,name,NTN,phone_number,email_address,vendor_address,website")] Vendor vendor)
        {

            var duplicate_email_check = _context.Vendors.Where(a => a.email_address == vendor.email_address).Count();
            if (duplicate_email_check == 0) { 
                if (ModelState.IsValid)
            {
                
                _context.Add(vendor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendor);
            }
            else
            {
                ViewBag.DuplicateEmail = "Cannot Create with existing email address";
                return View();
            }

        }

        // GET: Vendors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vendors == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }
            return View(vendor);
        }

        // POST: Vendors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("vendor_id,name,NTN,phone_number,email_address,vendor_address,website")] Vendor vendor)
        {
            if (id != vendor.vendor_id)
            {
                return NotFound();
            }
            var duplicate_email_check = _context.Vendors.Where(a => a.email_address == vendor.email_address).Count();
            if (ModelState.IsValid)
            {

                if (duplicate_email_check == 0) { 
                try
                {
                    _context.Update(vendor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorExists(vendor.vendor_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                }
                else
                {
                    ViewBag.Error = "Cannot Edit Data with existing Email Address";
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vendor);
        }

        // GET: Vendors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vendors == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors
                .FirstOrDefaultAsync(m => m.vendor_id == id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // POST: Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vendors == null)
            {
                return Problem("Entity set 'FYPContext.Vendors'  is null.");
            }
            var result=_context.Purchase_Orders.Where(s=>s.vendor_id==id).Count();
            if (result == 0) 
            { 
                var vendor = await _context.Vendors.FindAsync(id);
                if (vendor != null)
                {
                    _context.Vendors.Remove(vendor);
                }
            
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Error = "This Vendor cannot be Deleted";
                return View();
            }
        }

        //[AcceptVerbs("Get", "Post")]
        //public IActionResult IsEmailUnique(string email)
        //{
        //    var exists = _context.Vendors.Any(u => u.email_address== email);
        //    return Json(!exists);
        //}

        private bool VendorExists(int id)
        {
          return (_context.Vendors?.Any(e => e.vendor_id == id)).GetValueOrDefault();
        }
    }
}
