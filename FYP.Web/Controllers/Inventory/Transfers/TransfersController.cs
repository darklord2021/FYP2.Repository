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
using Microsoft.AspNetCore.Authorization;

namespace FYP.Web.Controllers.Inventory.Transfers
{
    [Authorize(Roles ="Admin,Inventory")]
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
            if (fYPContext is not null)
            {
                return View(await fYPContext.ToListAsync());
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> GRNIndex()
        {
            var fYPContext = _context.Transfers.Include(t => t.backorder_doc).Where(t=>t.Doc_name.Contains("WH-IN"));
            if (fYPContext is not null)
            {
                return View(await fYPContext.ToListAsync());
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> DNIndex()
        {
            var fYPContext = _context.Transfers.Include(t => t.backorder_doc).Where(t => t.Doc_name.Contains("WH-OUT"));
            if (fYPContext is not null)
            {
                return View(await fYPContext.ToListAsync());
            }
            else
            {
                return View();
            }
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
            

            if (id == null || _context.Transfers== null)
            {
                return NotFound();
            }

            var transfer = await _context.Transfers
                .Include(s=>s.Transfer_Details)
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
            
            if (id != viewModel.Transfer.ID)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            try
            {
                var originalTransfer = await _context.Transfers
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

                if (originalTransfer == null)
                {
                    return NotFound();
                }

                // Set the operation_type from the original entity to the modified entity
                viewModel.Transfer.operation_type = originalTransfer.operation_type;
                //viewModel.Transfer.operation_type
                //_context.Update(viewModel.Transfer);
                //await _context.SaveChangesAsync();
                //_context.Update(viewModel.LineItems);
                //await _context.SaveChangesAsync();

                // Update header items
                _context.Update(viewModel.Transfer);

                // Delete existing line items that are removed in the view
                var existingLineItems = await _context.Transfer_Details
                    .Where(d => d.transfer_id == id)
                    .ToListAsync();
                if (viewModel.LineItems == null)
                {
                    viewModel.LineItems = new List<FYP.DB.DBTables.Transfer_Detail>(); // Initialize the LineItems collection if it's null
                }
                foreach (var existingItem in existingLineItems)
                {
                    //if(existingItem is not null) { 
                    //if (!viewModel.LineItems.Any(li => li.transfer_id == id))
                    //{
                    //    _context.Transfer_Details.Remove(existingItem);
                    //}
                    //}
                    var updatedItem = viewModel.LineItems.FirstOrDefault(li => li.transfer_id == existingItem.transfer_id);

                    if (updatedItem != null)
                    {
                        existingItem.demand = updatedItem.demand; // Update the quantity
                        existingItem.done = updatedItem.done;
                    }
                    else
                    {
                        _context.Transfer_Details.Remove(existingItem);
                    }
                }

                // Add or update line items
                if ( viewModel.LineItems.Count != 0) { 
                foreach (var lineItem in viewModel.LineItems)
                {
                    if (lineItem.transfer_id == 0 || lineItem.transfer_id == null) // New line item, not in the database
                    {
                        _context.Transfer_Details.Add(lineItem);
                    }
                    else // Existing line item, update it
                    {
                        _context.Update(lineItem);
                    }
                }
                }
                else
                {
                    foreach (var lineItem in viewModel.LineItems)
                    {
                        if (lineItem.transfer_id == 0) // New line item, not in the database
                        {
                            lineItem.transfer_id = id;
                            _context.Transfer_Details.Add(lineItem);
                        }
                    }
                        //_context.Transfer_Details.Add(lineItem);
                }

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
                if (transfer.status == "Done")
                {
                    _context.Transfers.Remove(transfer);
                await _context.SaveChangesAsync();
                }
                else
                {
                    ViewBag.Error = "Transfer is already Validated";
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TransferExists(int id)
        {
          return (_context.Transfers?.Any(e => e.ID == id)).GetValueOrDefault();
        }


        //private bool CheckAvailability(int id)
        //{
        //    var transfer = _context.Transfers
        //        .Include(t => t.Transfer_Details)
        //        .FirstOrDefault(t => t.ID == id);

        //    foreach (var item in transfer.Transfer_Details)
        //    {
        //        var product = _context.Products.FirstOrDefault(a => a.product_id == item.product_id);

        //        if (item.demand > product.quantity)
        //        {
        //            // Set a message to display which product has insufficient quantity
        //            TempData["SalePopupMessage"] = $"Product '{product.name}' does not have sufficient quantity.";
        //            return false;
        //        }
        //    }

        //    return true; // All products have sufficient quantity
        //}
        //---------------------------------------------------------------------------------------------------------------------------------------

        //[HttpGet]
        //public async Task<IActionResult> Validate(int id)
        //{
        //    var transfer = await _context.Transfers
        //        .Include(tr => tr.Transfer_Details)
        //        .FirstOrDefaultAsync(tr => tr.ID == id);

        //    if (transfer == null)
        //    {
        //        return NotFound();
        //    }

        //    // Check if the transfer is already confirmed
        //    if (transfer.status == "Done")
        //    {
        //        TempData["SalePopupMessage"] = "Transfer is already validated.";
        //        return RedirectToAction(nameof(Index));
        //    }

        //    // Check availability before confirming the transfer
        //    List<FYP.DB.DBTables.Product> insufficientProducts;
        //    if (!CheckAvailability(id, out insufficientProducts))
        //    {
        //        TempData["InsufficientProducts"] = insufficientProducts;
        //        TempData["TransferId"] = id;
        //        return RedirectToAction(nameof(CreateBackorder));
        //    }

        //    // Update the stock based on the operation type (sale or purchase)
        //    foreach (var item in transfer.Transfer_Details)
        //    {
        //        var product = _context.Products.FirstOrDefault(p => p.product_id == item.product_id);
        //        if (product != null)
        //        {
        //            if (transfer.operation_type == "DN")
        //            {
        //                if(CheckAvailability(item.id) == true) { 
        //                product.quantity -= item.demand; // Reduce stock for sale
        //                }
        //            }
        //            else if (transfer.operation_type == "GRN")
        //            {
        //                product.quantity += item.demand; // Increase stock for purchase
        //            }
        //        }
        //    }

        //    // Update the status to "Done" and save changes

        //    transfer.status = "Done";
        //    _context.Update(transfer);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}

        private bool CheckAvailability(int id, out List<int> insufficientProductIds)
        {
            var transfer = _context.Transfers
                .Include(t => t.Transfer_Details)
                .FirstOrDefault(t => t.ID == id);

            insufficientProductIds = new List<int>();

            foreach (var item in transfer.Transfer_Details)
            {
                var product = _context.Products.FirstOrDefault(a => a.product_id == item.product_id);

                if (item.demand > product.quantity)
                {
                    insufficientProductIds.Add(product.product_id);
                    _context.SaveChanges();
                }
            }

            return insufficientProductIds.Count == 0; // All products have sufficient quantity
        }



        [HttpGet]
        public async Task<IActionResult> Validate(int id)
        {
            var transfer = await _context.Transfers
                .Include(tr => tr.Transfer_Details)
                .FirstOrDefaultAsync(tr => tr.ID == id);

            if (transfer == null)
            {
                return NotFound();
            }

            // Check if the transfer is already confirmed
            if (transfer.status == "Done")
            {
                TempData["SalePopupMessage"] = "Transfer is already validated.";
                return RedirectToAction(nameof(Index));
            }

            // Check availability before confirming the transfer
            //List<int> insufficientProductIds;
            //if (!CheckAvailability(id, out insufficientProductIds))
            //{
            //    TempData["InsufficientProductIds"] = insufficientProductIds;
            //    TempData["TransferId"] = id;
            //    return RedirectToAction(nameof(CreateBackorder));
            //}

            // Update the stock based on the operation type (sale or purchase)
            foreach (var item in transfer.Transfer_Details)
            {
                var product = _context.Products.FirstOrDefault(p => p.product_id == item.product_id);
                if (product != null)
                {
                    
                    if (transfer.operation_type == "DN" && item.demand <= product.quantity)
                    {
                        product.quantity -= item.demand; // Reduce stock for sale
                    }
                    else if (transfer.operation_type == "GRN")
                    {
                        product.quantity += item.done; // Increase stock for purchase
                    }
                }
            }

            // Update the status to "Done" and save changes
            transfer.status = "Done";
            _context.Update(transfer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        // In your action method



        [HttpGet]
        public IActionResult CreateBackorder()
        {
            var insufficientProducts = TempData["InsufficientProducts"] as List<FYP.DB.DBTables.Product>;
            var transferId = (int)TempData["TransferId"];

            if (insufficientProducts == null || insufficientProducts.Count == 0)
            {
                // If the user accessed the CreateBackorder action directly without going through the Validate action,
                // redirect to the Transfer Index.
                return RedirectToAction(nameof(Index));
            }

            ViewBag.TransferId = transferId;
            ViewBag.InsufficientProducts = insufficientProducts;
            return View();
        }


    }
}
