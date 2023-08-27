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
using FYP.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace FYP.Web.Controllers.Purchase
{
    [Authorize(Roles =("Admin,Purchase"))]
    public class Purchase_OrderController : Controller
    {
        private int count = 0;

        private readonly FYPContext _context;

        public Purchase_OrderController(FYPContext context)
        {
            _context = context;
        }

        // GET: Purchase_Order
        public async Task<IActionResult> Index()
        {
            try { 
                
            var fYPContext = _context.Purchase_Orders.Include(p => p.payment_methodNavigation).Include(p => p.vendor).OrderBy(x=>x.purchase_id);
                
                if(fYPContext is not null) 
                { 
                    return View(await fYPContext.ToListAsync());
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {

                return View("Error", new ErrorViewModel
                {
                    ErrorMessage = "An error occurred while processing your request."+e.Message
                });
            }
        }

        // GET: Purchase_Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var purchase_Order = await _context.Purchase_Orders
                    .Include(s => s.vendor)
                    .Include(s => s.payment_methodNavigation)
                    .FirstOrDefaultAsync(m => m.purchase_id == id);

                if (purchase_Order == null)
                {
                    return NotFound();
                }

                var viewModel = new PurchaseViewModel
                {
                    PurchaseOrder = purchase_Order,
                    PurchaseItems = await _context.Purchase_Order_Details
                    .Include(d => d.product)
                .Where(d => d.purchase_id == id)
                .ToListAsync()
                };
                foreach (var item in viewModel.PurchaseItems) 
                {
                    Console.WriteLine(item.ID);
                }

                return View(viewModel);
            }
            catch (Exception e)
            {

                return View("Error", new ErrorViewModel
                {
                    ErrorMessage = "An error occurred while processing your request." +e.Message
                });
            }
        }
        // GET: Purchase_Order/Create
        public IActionResult Create()
        {
            try
            {
                ViewData["payment_method"] = new SelectList(_context.Payments, "id", "method_name");
                ViewData["vendor_id"] = new SelectList(_context.Vendors, "vendor_id", "name");

                ViewData["product_id"] = new SelectList(_context.Products, "product_id", "name");
                var viewModel = new PurchaseViewModel
                {
                    PurchaseOrder = new Purchase_Order(),
                    PurchaseItems = new List<Purchase_Order_Detail>(),
                };
                return View(viewModel);
            }
            catch (Exception e)
            {

                return View("Error", new ErrorViewModel
                {
                    ErrorMessage = "An error occurred while processing your request." + e.Message
                });
            }
        }
        // POST: Purchase_Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseViewModel viewModel)
        {
            //try
            //{
                // Save the sale order header to the database
                //viewModel.PurchaseOrder.doc_name = string.Format("P{0:D5}", _context.Purchase_Orders.Count() + 1);
                string? maxExistingName = _context.Purchase_Orders
                                       .Select(so => so.doc_name)
                                       .OrderByDescending(name =>name)
                                       .FirstOrDefault();
            int count = 0;
            if (!string.IsNullOrEmpty(maxExistingName) && maxExistingName.Length >= 2)
                {
                    if (int.TryParse(maxExistingName.Substring(1), out int numericPart))
                    {
                        count = numericPart + 1;
                    }
                }

                // Generate the new name
                string newUniqueName = string.Format("P{0:D5}", count);

                // Set the new unique name
                viewModel.PurchaseOrder.doc_name = newUniqueName;
                viewModel.PurchaseOrder.create_date = DateTime.Today.Date;
                _context.Purchase_Orders.Add(viewModel.PurchaseOrder);
                await _context.SaveChangesAsync();

                // Associate the line items with the sale order
                if (viewModel.PurchaseItems is not null)
                {
                    foreach (var item in viewModel.PurchaseItems)
                    {
                        var lineItem = new DB.DBTables.Purchase_Order_Detail
                        {
                            purchase_id = viewModel.PurchaseOrder.purchase_id,
                            product_id = item.product_id,
                            quantity = item.quantity,
                            price = item.price
                        };
                        //if (lineItem.quantity < lineItem.product.quantity)
                        //{
                        _context.Purchase_Order_Details.Add(lineItem);
                        await _context.SaveChangesAsync();
                        //}
                    }
                }

                // Redirect to a success page or take appropriate action
                return RedirectToAction("Index");
                //}
                return View(viewModel);
            //}
            //catch (Exception e)
            //{

            //    return View("Error", new ErrorViewModel
            //    {
            //        ErrorMessage = "An error occurred while processing your request." + e.Message
            //    });
            //}
        }
        // GET: Purchase_Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {


                if (id == null || _context.Purchase_Orders == null)
                {
                    return NotFound();
                }

                var purchase_Order = await _context.Purchase_Orders
                                .Include(s => s.vendor)
                                .Include(s => s.payment_methodNavigation)
                                .FirstOrDefaultAsync(m => m.purchase_id == id);
                if (purchase_Order == null)
                {
                    return NotFound();
                }
                var purchaseItems = await _context.Purchase_Order_Details
            .Include(pd => pd.product)
            .Where(pd => pd.purchase_id == id)
            .ToListAsync();
                var viewModel = new PurchaseViewModel
                {
                    PurchaseOrder = purchase_Order,
                    PurchaseItems = purchaseItems
                };


                ViewData["product_id"] = new SelectList(_context.Products, "product_id", "name");

                ViewData["vendor_id"] = new SelectList(_context.Vendors, "vendor_id", "name", purchase_Order.vendor_id);
                //ViewData["customer_id"] = new SelectList(_context.Customers, "customer_id", "customer_id", purchase_Order.vendor_id);
                ViewData["payment_method"] = new SelectList(_context.Payments, "id", "method_name", purchase_Order.payment_method);
                return View(viewModel);
            }
            catch (Exception e)
            {

                return View("Error", new ErrorViewModel
                {
                    ErrorMessage = "An error occurred while processing your request." + e
                });
            }
        }
        // POST: Purchase_Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PurchaseViewModel viewModel)
        {
            try
            {

                if (id != viewModel.PurchaseOrder.purchase_id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            try
            {
                    var existingPurchaseItems = _context.Purchase_Order_Details
                                .Where(item => item.purchase_id == viewModel.PurchaseOrder.purchase_id)
                                .ToList(); 
                    foreach (var existingPurchaseItem in existingPurchaseItems)
                    {
                        _context.Purchase_Order_Details.Remove(existingPurchaseItem);
                    }

                    _context.Update(viewModel.PurchaseOrder);
                    await _context.SaveChangesAsync();

                    //foreach (var purchase in viewModel.PurchaseItems) 
                    //{
                    //            purchase.purchase_id = viewModel.PurchaseOrder.purchase_id;
                    //            purchase.ID = purchase.ID;
                    //            _context.Update(purchase);
                    //            _context.SaveChanges();
                    //}

                    if (viewModel.PurchaseItems is not null)
                    {
                        foreach (var item in viewModel.PurchaseItems)
                        {
                            var lineItem = new DB.DBTables.Purchase_Order_Detail
                            {
                                purchase_id = viewModel.PurchaseOrder.purchase_id,
                                product_id = item.product_id,
                                quantity = item.quantity,
                                price = item.price
                            };
                            //if (lineItem.quantity < lineItem.product.quantity)
                            //{
                            _context.Purchase_Order_Details.Add(lineItem);
                            _context.SaveChanges();
                            //}
                        }
                    }

                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!Purchase_OrderExists(viewModel.PurchaseOrder.purchase_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw e;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["customer_id"] = new SelectList(_context.Customers, "customer_id", "customer_id", viewModel.PurchaseOrder.vendor_id);
            ViewData["payment_method"] = new SelectList(_context.Payments, "id", "method_name", viewModel.PurchaseOrder.purchase_id);
            return View(viewModel);
        }
            catch (Exception e)
            {

                return View("Error", new ErrorViewModel
                {
                    ErrorMessage = "An error occurred while processing your request." + e.Message
                });
            }
        }

        // GET: Purchase_Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Purchase_Orders == null)
            {
                return NotFound();
            }

            var purchase_Order = await _context.Purchase_Orders
                .Include(p => p.payment_methodNavigation)
                .Include(p => p.vendor)
                .FirstOrDefaultAsync(m => m.purchase_id == id);
            if (purchase_Order == null)
            {
                return NotFound();
            }

            return View(purchase_Order);
        }

        // POST: Purchase_Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Purchase_Orders == null)
            {
                return Problem("Entity set 'FYPContext.Purchase_Orders'  is null.");
            }
            var purchase_Order = await _context.Purchase_Orders.FindAsync(id);
            if (purchase_Order != null)
            {
                _context.Purchase_Orders.Remove(purchase_Order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Purchase_OrderExists(int id)
        {
                return (_context.Purchase_Orders?.Any(e => e.purchase_id == id)).GetValueOrDefault();
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        //Creates GRN and confirms PO
        public async Task<IActionResult> ConfirmPurchaseOrder(int id)
        {
            try
            {
                // Find the sale order by ID
                var purchaseOrder = await _context.Purchase_Orders
                    .Include(po => po.Purchase_Order_Details)
                    .FirstOrDefaultAsync(po => po.purchase_id == id);

                if (purchaseOrder == null)
                {
                    return NotFound();
                }

                // Check if the sale order is already confirmed
                if (purchaseOrder.state == "Purchase Order")
                {
                    //return BadRequest("Sale order is already confirmed.");
                    TempData["SalePopupMessage"] = "Purchase order is already confirmed.";
                    return RedirectToAction(nameof(Index));
                }

                // Update the state to "Purchase"
                purchaseOrder.state = "Purchase Order";
                _context.Update(purchaseOrder);
                await _context.SaveChangesAsync();

                // Add items to the transfer table
                var transfer = new DB.DBTables.Transfer
                {
                    Doc_name = "WH-IN/" + DateTime.Now.Date.Year + "/" + DateTime.Now.Date.Month + "/" + DateTime.Now.Date.Day + "/" + purchaseOrder.doc_name,
                    Source_Document = purchaseOrder.doc_name,
                    created_date = DateTime.Now.Date,
                    status = "Initial",
                    operation_type = "GRN"
                };

                _context.Transfers.Add(transfer);
                await _context.SaveChangesAsync();

                // Add details to the transfer detail table
                foreach (var item in purchaseOrder.Purchase_Order_Details)
                {
                    var transferDetail = new DB.DBTables.Transfer_Detail
                    {
                        transfer_id = transfer.ID,
                        product_id = (int)item.product_id,
                        demand = (int)item.quantity,
                        done = 0
                    };

                    _context.Transfer_Details.Add(transferDetail);
                }

                await _context.SaveChangesAsync();
                TempData["SalePopupMessage"] = "Sale Order confirmed.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {

                return View("Error", new ErrorViewModel
                {
                    ErrorMessage = "An error occurred while processing your request."
                });
            }
        }

        //Creates Bill after GRN is Is complete
        public async Task<IActionResult> CreateBill(int id)
        {
            try { 
            // Find the sale order by ID
            var purchaseOrder = await _context.Purchase_Orders
                .FirstOrDefaultAsync(po => po.purchase_id == id);


            if (purchaseOrder == null)
            {
                return NotFound();
            }

            // Check if the sale order is already confirmed
            if (purchaseOrder.state == "Purchase Order")
            {

                var transfer = await _context.Transfers
                .FirstOrDefaultAsync(t => t.Source_Document == purchaseOrder.doc_name && t.status == "Done");
                //if transfer is done for that sale order

                if (transfer != null)
                {
                    if (transfer.status == "Done")
                    {
                        var invoice = new Account_Move
                        {
                            Doc_Name = "Bill/" + DateTime.Now.Date.Year + "/" + DateTime.Now.Date.Month + "/" + DateTime.Now.Date.Day + "/" + (++count),
                            purchase_source_doc = purchaseOrder.doc_name,
                            Date_Created = DateTime.Now.Date,
                            Status = "Draft",
                            operation_type = "Bill"
                        };

                        _context.Add(invoice);
                        await _context.SaveChangesAsync();
                        // Add details to the transfer detail table
                        foreach (var item in purchaseOrder.Purchase_Order_Details)
                        {
                            var invoicelines = new Invoice_line
                            {
                                account_id = invoice.ID,
                                product_id = item.product.product_id,
                                qty = (int)item.quantity,
                                price = (decimal)item.price,
                                taxes = .18
                            };

                            _context.Invoice_lines.Add(invoicelines);
                        }
                    }
                    else
                    {
                        //if transfer is not
                        TempData["TransferPopupMessage"] = "Invoice cannot be created when Goods are not Received.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    //if transfer is not found
                    TempData["TransferPopupMessage"] = "Transfer not found.";
                    return RedirectToAction(nameof(Index));
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
            catch (Exception e)
            {

                return View("Error", new ErrorViewModel
                {
                    ErrorMessage = "An error occurred while processing your request."
                });
            }
        }
    }
}
