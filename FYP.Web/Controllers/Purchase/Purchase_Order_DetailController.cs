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

namespace FYP.Web.Controllers.Purchase
{
    [Authorize(Roles =("Admin,Purchase"))]
    public class Purchase_Order_DetailController : Controller
    {
        private readonly FYPContext _context;

        public Purchase_Order_DetailController(FYPContext context)
        {
            _context = context;
        }

        // GET: Purchase_Order_Detail
        public async Task<IActionResult> Index()
        {
            var fYPContext = _context.Purchase_Order_Details.Include(p => p.product).Include(p => p.purchase);
            return View(await fYPContext.ToListAsync());
        }

        // GET: Purchase_Order_Detail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Purchase_Order_Details == null)
            {
                return NotFound();
            }

            var purchase_Order_Detail = await _context.Purchase_Order_Details
                .Include(p => p.product)
                .Include(p => p.purchase)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (purchase_Order_Detail == null)
            {
                return NotFound();
            }

            return View(purchase_Order_Detail);
        }

        // GET: Purchase_Order_Detail/Create
        public IActionResult Create()
        {
            ViewData["product_id"] = new SelectList(_context.Products, "product_id", "name");
            ViewData["purchase_id"] = new SelectList(_context.Purchase_Orders, "purchase_id", "doc_name");
            return View();
        }

        // POST: Purchase_Order_Detail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,purchase_id,product_id,quantity,price")] Purchase_Order_Detail purchase_Order_Detail)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(purchase_Order_Detail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["product_id"] = new SelectList(_context.Products, "product_id", "name", purchase_Order_Detail.product_id);
            ViewData["purchase_id"] = new SelectList(_context.Purchase_Orders, "purchase_id", "doc_name", purchase_Order_Detail.purchase_id);
            return View(purchase_Order_Detail);
        }

        // GET: Purchase_Order_Detail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Purchase_Order_Details == null)
            {
                return NotFound();
            }

            var purchase_Order_Detail = await _context.Purchase_Order_Details.FindAsync(id);
            if (purchase_Order_Detail == null)
            {
                return NotFound();
            }
            ViewData["product_id"] = new SelectList(_context.Products, "product_id", "name", purchase_Order_Detail.product_id);
            ViewData["purchase_id"] = new SelectList(_context.Purchase_Orders, "purchase_id", "doc_name", purchase_Order_Detail.purchase_id);
            return View(purchase_Order_Detail);
        }

        // POST: Purchase_Order_Detail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,purchase_id,product_id,quantity,price")] Purchase_Order_Detail purchase_Order_Detail)
        {
            if (id != purchase_Order_Detail.ID)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(purchase_Order_Detail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Purchase_Order_DetailExists(purchase_Order_Detail.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["product_id"] = new SelectList(_context.Products, "product_id", "name", purchase_Order_Detail.product_id);
            ViewData["purchase_id"] = new SelectList(_context.Purchase_Orders, "purchase_id", "doc_name", purchase_Order_Detail.purchase_id);
            return View(purchase_Order_Detail);
        }

        // GET: Purchase_Order_Detail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Purchase_Order_Details == null)
            {
                return NotFound();
            }

            var purchase_Order_Detail = await _context.Purchase_Order_Details
                .Include(p => p.product)
                .Include(p => p.purchase)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (purchase_Order_Detail == null)
            {
                return NotFound();
            }

            return View(purchase_Order_Detail);
        }

        // POST: Purchase_Order_Detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Purchase_Order_Details == null)
            {
                return Problem("Entity set 'FYPContext.Purchase_Order_Details'  is null.");
            }
            var purchase_Order_Detail = await _context.Purchase_Order_Details.FindAsync(id);
            if (purchase_Order_Detail != null)
            {
                _context.Purchase_Order_Details.Remove(purchase_Order_Detail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Purchase_Order_DetailExists(int id)
        {
          return (_context.Purchase_Order_Details?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
