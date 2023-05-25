using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FYP.DB.Context;
using FYP.DB.DBTables;

namespace FYP.Web.Controllers.Sales
{
    public class Sale_Order_DetailController : Controller
    {
        private readonly FYPContext _context;

        public Sale_Order_DetailController(FYPContext context)
        {
            _context = context;
        }

        // GET: Sale_Order_Detail
        public async Task<IActionResult> Index()
        {
            var fYPContext = _context.Sale_Order_Details.Include(s => s.product).Include(s => s.sale);
            return View(await fYPContext.ToListAsync());
        }

        // GET: Sale_Order_Detail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sale_Order_Details == null)
            {
                return NotFound();
            }

            var sale_Order_Detail = await _context.Sale_Order_Details
                .Include(s => s.product)
                .Include(s => s.sale)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (sale_Order_Detail == null)
            {
                return NotFound();
            }

            return View(sale_Order_Detail);
        }

        // GET: Sale_Order_Detail/Create
        public IActionResult Create()
        {
            ViewData["product_id"] = new SelectList(_context.Products, "product_id", "name");
            ViewData["sale_id"] = new SelectList(_context.Sale_Orders, "sale_id", "name");
            return View();
        }

        // POST: Sale_Order_Detail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,sale_id,product_id,quantity,price")] Sale_Order_Detail sale_Order_Detail)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(sale_Order_Detail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["product_id"] = new SelectList(_context.Products, "product_id", "name", sale_Order_Detail.product_id);
            ViewData["sale_id"] = new SelectList(_context.Sale_Orders, "sale_id", "name", sale_Order_Detail.sale_id);
            return View(sale_Order_Detail);
        }

        // GET: Sale_Order_Detail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sale_Order_Details == null)
            {
                return NotFound();
            }

            var sale_Order_Detail = await _context.Sale_Order_Details.FindAsync(id);
            if (sale_Order_Detail == null)
            {
                return NotFound();
            }
            ViewData["product_id"] = new SelectList(_context.Products, "product_id", "name", sale_Order_Detail.product_id);
            ViewData["sale_id"] = new SelectList(_context.Sale_Orders, "sale_id", "name", sale_Order_Detail.sale_id);
            return View(sale_Order_Detail);
        }

        // POST: Sale_Order_Detail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,sale_id,product_id,quantity,price")] Sale_Order_Detail sale_Order_Detail)
        {
            if (id != sale_Order_Detail.ID)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(sale_Order_Detail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Sale_Order_DetailExists(sale_Order_Detail.ID))
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
            ViewData["product_id"] = new SelectList(_context.Products, "product_id", "name", sale_Order_Detail.product_id);
            ViewData["sale_id"] = new SelectList(_context.Sale_Orders, "sale_id", "name", sale_Order_Detail.sale_id);
            return View(sale_Order_Detail);
        }

        // GET: Sale_Order_Detail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sale_Order_Details == null)
            {
                return NotFound();
            }

            var sale_Order_Detail = await _context.Sale_Order_Details
                .Include(s => s.product)
                .Include(s => s.sale)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (sale_Order_Detail == null)
            {
                return NotFound();
            }

            return View(sale_Order_Detail);
        }

        // POST: Sale_Order_Detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sale_Order_Details == null)
            {
                return Problem("Entity set 'FYPContext.Sale_Order_Details'  is null.");
            }
            var sale_Order_Detail = await _context.Sale_Order_Details.FindAsync(id);
            if (sale_Order_Detail != null)
            {
                _context.Sale_Order_Details.Remove(sale_Order_Detail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Sale_Order_DetailExists(int id)
        {
          return (_context.Sale_Order_Details?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
