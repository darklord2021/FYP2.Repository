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
    public class TransfersController : Controller
    {
        private readonly FYPContext _context;

        public TransfersController(FYPContext context)
        {
            _context = context;
        }

        // GET: Transfers
        public async Task<IActionResult> Index()
        {
            var fYPContext = _context.Transfers.Include(t => t.backorder_doc);
            return View(await fYPContext.ToListAsync());
        }

        // GET: Transfers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transfers == null)
            {
                return NotFound();
            }

            var transfer = await _context.Transfers
                .Include(t => t.backorder_doc)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (transfer == null)
            {
                return NotFound();
            }

            return View(transfer);
        }

        // GET: Transfers/Create
        public IActionResult Create()
        {
            ViewData["backorder_doc_id"] = new SelectList(_context.Transfers, "ID", "status");
            return View();
        }

        // POST: Transfers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Doc_name,Source_Document,created_date,backorder_doc_id,status")] Transfer transfer)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(transfer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["backorder_doc_id"] = new SelectList(_context.Transfers, "ID", "status", transfer.backorder_doc_id);
            return View(transfer);
        }

        // GET: Transfers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transfers == null)
            {
                return NotFound();
            }

            var transfer = await _context.Transfers.FindAsync(id);
            if (transfer == null)
            {
                return NotFound();
            }
            ViewData["backorder_doc_id"] = new SelectList(_context.Transfers, "ID", "status", transfer.backorder_doc_id);
            return View(transfer);
        }

        // POST: Transfers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Doc_name,Source_Document,created_date,backorder_doc_id,status")] Transfer transfer)
        {
            if (id != transfer.ID)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(transfer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransferExists(transfer.ID))
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
            ViewData["backorder_doc_id"] = new SelectList(_context.Transfers, "ID", "status", transfer.backorder_doc_id);
            return View(transfer);
        }

        // GET: Transfers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transfers == null)
            {
                return NotFound();
            }

            var transfer = await _context.Transfers
                .Include(t => t.backorder_doc)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (transfer == null)
            {
                return NotFound();
            }

            return View(transfer);
        }

        // POST: Transfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transfers == null)
            {
                return Problem("Entity set 'FYPContext.Transfers'  is null.");
            }
            var transfer = await _context.Transfers.FindAsync(id);
            if (transfer != null)
            {
                _context.Transfers.Remove(transfer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransferExists(int id)
        {
          return (_context.Transfers?.Any(e => e.ID == id)).GetValueOrDefault();
        }






        //private bool checkavailability(int id)
        //{
        //    var transfer = _context.Transfers
        //        .Include(t => t.TransferDetails)
        //        .FirstOrDefault(t => t.Id == id);

        //    int tcount = 0;
        //    foreach (var item in transfer.TransferDetails)
        //    {
        //        var n = transfer.TransferDetails;
        //        if(item.Demand< )
        //    }
        //}

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Validate(int id)
        {
            // Find the sale order by ID
            var transfer = await _context.Transfers
                .Include(tr => tr.Transfer_Details)
                .FirstOrDefaultAsync(tr => tr.ID == id);

            if (transfer == null)
            {
                return NotFound();
            }

            // Check if the sale order is already confirmed
            if (transfer.status == "Done")
            {
                //return BadRequest("Sale order is already confirmed.");
                TempData["SalePopupMessage"] = "Transfer is already validated.";
                return RedirectToAction(nameof(Index));
            }

            // Update the state to "Sale"
            transfer.status = "Done";
            _context.Update(transfer);
            await _context.SaveChangesAsync();
            int count = 0;


            // Add details to the transfer detail table
            foreach (var item in transfer.Transfer_Details)
            {
                var transferDetail = new Transfer_Detail
                {
                    transfer_id = transfer.ID,
                    product_id = item.product_id,
                    demand = item.demand,
                    done = 0
                };
                if (transferDetail.demand > transferDetail.product.quantity)
                {
                    count++;
                }


            }
            if (count > 0)
            {
                await _context.SaveChangesAsync();
                TempData["SalePopupMessage"] = "cannot Validate.";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
