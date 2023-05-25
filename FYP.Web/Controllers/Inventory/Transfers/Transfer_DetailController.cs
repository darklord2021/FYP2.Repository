using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FYP.DB.Context;
using FYP.DB.DBTables;

namespace FYP.Web.Controllers.Inventory.Transfers
{
    public class Transfer_DetailController : Controller
    {
        private readonly FYPContext _context;

        public Transfer_DetailController(FYPContext context)
        {
            _context = context;
        }

        // GET: Transfer_Detail
        public async Task<IActionResult> Index()
        {
            var fYPContext = _context.Transfer_Details.Include(t => t.product).Include(t => t.transfer);
            return View(await fYPContext.ToListAsync());
        }

        // GET: Transfer_Detail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transfer_Details == null)
            {
                return NotFound();
            }

            var transfer_Detail = await _context.Transfer_Details
                .Include(t => t.product)
                .Include(t => t.transfer)
                .FirstOrDefaultAsync(m => m.id == id);
            if (transfer_Detail == null)
            {
                return NotFound();
            }

            return View(transfer_Detail);
        }

        // GET: Transfer_Detail/Create
        public IActionResult Create()
        {
            ViewData["product_id"] = new SelectList(_context.Products, "product_id", "name");
            ViewData["transfer_id"] = new SelectList(_context.Transfers, "ID", "status");
            return View();
        }

        // POST: Transfer_Detail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,transfer_id,product_id,demand,done")] Transfer_Detail transfer_Detail)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(transfer_Detail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["product_id"] = new SelectList(_context.Products, "product_id", "name", transfer_Detail.product_id);
            ViewData["transfer_id"] = new SelectList(_context.Transfers, "ID", "status", transfer_Detail.transfer_id);
            return View(transfer_Detail);
        }

        // GET: Transfer_Detail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transfer_Details == null)
            {
                return NotFound();
            }

            var transfer_Detail = await _context.Transfer_Details.FindAsync(id);
            if (transfer_Detail == null)
            {
                return NotFound();
            }
            ViewData["product_id"] = new SelectList(_context.Products, "product_id", "name", transfer_Detail.product_id);
            ViewData["transfer_id"] = new SelectList(_context.Transfers, "ID", "status", transfer_Detail.transfer_id);
            return View(transfer_Detail);
        }

        // POST: Transfer_Detail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,transfer_id,product_id,demand,done")] Transfer_Detail transfer_Detail)
        {
            if (id != transfer_Detail.id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(transfer_Detail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Transfer_DetailExists(transfer_Detail.id))
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
            ViewData["product_id"] = new SelectList(_context.Products, "product_id", "name", transfer_Detail.product_id);
            ViewData["transfer_id"] = new SelectList(_context.Transfers, "ID", "status", transfer_Detail.transfer_id);
            return View(transfer_Detail);
        }

        // GET: Transfer_Detail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transfer_Details == null)
            {
                return NotFound();
            }

            var transfer_Detail = await _context.Transfer_Details
                .Include(t => t.product)
                .Include(t => t.transfer)
                .FirstOrDefaultAsync(m => m.id == id);
            if (transfer_Detail == null)
            {
                return NotFound();
            }

            return View(transfer_Detail);
        }

        // POST: Transfer_Detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transfer_Details == null)
            {
                return Problem("Entity set 'FYPContext.Transfer_Details'  is null.");
            }
            var transfer_Detail = await _context.Transfer_Details.FindAsync(id);
            if (transfer_Detail != null)
            {
                _context.Transfer_Details.Remove(transfer_Detail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Transfer_DetailExists(int id)
        {
          return (_context.Transfer_Details?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
