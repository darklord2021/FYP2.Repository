using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FYP.DB.Context;
using FYP.DB.DBTables;
using FYP.DB.ViewModels;

namespace FYP.Web.Controllers.Inventory.Transfers
{
    public class TransfersController : Controller
    {
        private readonly FYPContext _context;
        private bool available;

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

        public async Task<IActionResult> GRNIndex()
        {
            var fYPContext = _context.Transfers.Include(t => t.backorder_doc).Where(t=>t.Doc_name.Contains("WH-IN"));
            return View(await fYPContext.ToListAsync());
        }
        public async Task<IActionResult> DNIndex()
        {
            var fYPContext = _context.Transfers.Include(t => t.backorder_doc).Where(t => t.Doc_name.Contains("WH-OUT"));
            return View(await fYPContext.ToListAsync());
        }

        // GET: Transfers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transfer = await _context.Transfers
                .FirstOrDefaultAsync(m => m.ID == id);

            if (transfer == null)
            {
                return NotFound();
            }

            var viewModel = new TransferViewModel
            {
                Transfer = transfer,
                LineItems = await _context.Transfer_Details
                .Include(d => d.product)
            .Where(d => d.transfer_id == id)
            .ToListAsync()
            };

            return View(viewModel);
        }

        // GET: Transfers/Create
        public IActionResult Create()
        {
            //return View();

            ViewData["backorder_doc_id"] = new SelectList(_context.Transfers, "ID", "status");
            //ViewData["customer_id"] = new SelectList(_context.Customers, "customer_id", "customer_name");
            ViewData["payment_method"] = new SelectList(_context.Payments, "id", "method_name");
            ViewData["product_id"] = new SelectList(_context.Products, "product_id", "name");
            var viewModel = new TransferViewModel
            {
                Transfer = new DB.DBTables.Transfer(),
                LineItems = new List<DB.DBTables.Transfer_Detail>(),
            };

            return View(viewModel);
        }

        // POST: Transfers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransferViewModel viewModel)
        {
            ////if (ModelState.IsValid)
            ////{
            //    _context.Add(transfer);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            ////}
            //ViewData["backorder_doc_id"] = new SelectList(_context.Transfers, "ID", "status", transfer.backorder_doc_id);
            //return View(transfer);


            _context.Transfers.Add(viewModel.Transfer);
            _context.SaveChanges();

            // Associate the line items with the sale order

            foreach (var item in viewModel.LineItems)
            {
                var lineItem = new DB.DBTables.Transfer_Detail
                {
                    transfer_id = viewModel.Transfer.ID,
                    product_id = item.product_id,
                    demand = item.demand,
                    done = item.done
                };
                //if (lineItem.quantity < lineItem.product.quantity)
                //{
                _context.Transfer_Details.Add(lineItem);
                _context.SaveChanges();
                //}
            }


            // Redirect to a success page or take appropriate action
            return RedirectToAction("Index");
            //}
            return View(viewModel);
        }

        // GET: Transfers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null || _context.Transfers == null)
            //{
            //    return NotFound();
            //}

            //var transfer = await _context.Transfers.FindAsync(id);
            //if (transfer == null)
            //{
            //    return NotFound();
            //}
            //ViewData["backorder_doc_id"] = new SelectList(_context.Transfers, "ID", "status", transfer.backorder_doc_id);
            //return View(transfer);

            if (id == null || _context.Transfers== null)
            {
                return NotFound();
            }

            var transfer = await _context.Transfers
                            //.Include(s => s.customer)
                            //.Include(s => s.payment_methodNavigation)
                            .FirstOrDefaultAsync(m => m.ID == id);
            if (transfer == null)
            {
                return NotFound();
            }

            var viewModel = new TransferViewModel
            {
                 Transfer= transfer,
                LineItems = await _context.Transfer_Details
                .Include(d => d.product)
            .Where(d => d.transfer_id == id)
            .ToListAsync()
            };
            ViewData["product_id"] = new SelectList(_context.Products, "product_id", "name");

            //ViewData["customer_id"] = new SelectList(_context.Customers, "customer_id", "customer_id", sale_Order.customer_id);
            //ViewData["payment_method"] = new SelectList(_context.Payments, "id", "method_name", sale_Order.payment_method);
            return View(viewModel);
        }

        // POST: Transfers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,Doc_name,Source_Document,created_date,backorder_doc_id,status")] Transfer transfer)
        public async Task<IActionResult> Edit(int id, TransferViewModel viewModel)
        {
            //if (id != transfer.ID)
            //{
            //    return NotFound();
            //}

            ////if (ModelState.IsValid)
            ////{
            //    try
            //    {
            //        _context.Update(transfer);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!TransferExists(transfer.ID))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            ////}
            //ViewData["backorder_doc_id"] = new SelectList(_context.Transfers, "ID", "status", transfer.backorder_doc_id);
            //return View(transfer);

            if (id != viewModel.Transfer.ID)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            try
            {
                _context.Update(viewModel.Transfer);
                await _context.SaveChangesAsync();
                _context.Update(viewModel.LineItems);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransferExists(viewModel.Transfer.ID))
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
            //ViewData["customer_id"] = new SelectList(_context.Customers, "customer_id", "customer_id", viewModel.SaleOrder.customer_id);
            //ViewData["payment_method"] = new SelectList(_context.Payments, "id", "method_name", viewModel.SaleOrder.payment_method);
            return View(viewModel);
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
        //        .Include(t => t.Transfer_Details)
        //        .FirstOrDefault(t => t.ID == id);
        //    int tcount = 0;
        //    foreach (var item in transfer.Transfer_Details)
        //    {
        //        var products = _context.Products.FirstOrDefault(a=> a.product_id == item.product_id);
        //        var n = transfer.Transfer_Details;

        //        if (item.demand < products.quantity)
        //        {
        //            available = true;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //}

        private bool CheckAvailability(int id)
        {
            var transfer = _context.Transfers
                .Include(t => t.Transfer_Details)
                .FirstOrDefault(t => t.ID == id);

            foreach (var item in transfer.Transfer_Details)
            {
                var product = _context.Products.FirstOrDefault(a => a.product_id == item.product_id);

                if (item.demand >= product.quantity)
                {
                    // Exclude the quantity that doesn't satisfy the availability condition
                    item.demand = product.quantity - 1;
                }
            }

            return true;
        }


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
                Product pq = _context.Products.Find(transferDetail.product_id);
                //check availability
                if (transferDetail.demand > pq.quantity)
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
