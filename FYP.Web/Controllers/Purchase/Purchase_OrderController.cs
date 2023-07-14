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

namespace FYP.Web.Controllers.Purchase
{
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
            var fYPContext = _context.Purchase_Orders.Include(p => p.payment_methodNavigation).Include(p => p.vendor);
            return View(await fYPContext.ToListAsync());
        }

        // GET: Purchase_Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (id == null || _context.Purchase_Orders == null)
            //{
            //    return NotFound();
            //}

            //var purchase_Order = await _context.Purchase_Orders
            //    .Include(p => p.payment_methodNavigation)
            //    .Include(p => p.vendor)
            //    .FirstOrDefaultAsync(m => m.purchase_id == id);
            //if (purchase_Order == null)
            //{
            //    return NotFound();
            //}

            //return View(purchase_Order);

            if (id == null)
            {
                return NotFound();
            }

            var purchase_Order = await _context.Purchase_Orders
                .Include(s => s.vendor)
                .Include(s => s.payment_methodNavigation)
                .FirstOrDefaultAsync(m => m.purchase_id== id);

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

            return View(viewModel);
        }

        // GET: Purchase_Order/Create
        public IActionResult Create()
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

        // POST: Purchase_Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("purchase_id,vendor_id,doc_name,cost,create_date,state,payment_method")] Purchase_Order purchase_Order)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //        _context.Add(purchase_Order);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    //}
        //    ViewData["payment_method"] = new SelectList(_context.Payments, "id", "method_name", purchase_Order.payment_method);
        //    ViewData["vendor_id"] = new SelectList(_context.Vendors, "vendor_id", "name", purchase_Order.vendor_id);
        //    return View(purchase_Order);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseViewModel viewModel)
        {
            // Save the sale order header to the database
            _context.Purchase_Orders.Add(viewModel.PurchaseOrder);
            _context.SaveChanges();

            // Associate the line items with the sale order

            foreach (var item in viewModel.PurchaseItems)
            {
                var lineItem = new Purchase_Order_Detail
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


            // Redirect to a success page or take appropriate action
            return RedirectToAction("Index");
            //}
            return View(viewModel);
        }


        // GET: Purchase_Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null || _context.Purchase_Orders == null)
            //{
            //    return NotFound();
            //}

            //var purchase_Order = await _context.Purchase_Orders.FindAsync(id);
            //if (purchase_Order == null)
            //{
            //    return NotFound();
            //}
            //ViewData["payment_method"] = new SelectList(_context.Payments, "id", "method_name", purchase_Order.payment_method);
            //ViewData["vendor_id"] = new SelectList(_context.Vendors, "vendor_id", "name", purchase_Order.vendor_id);
            //return View(purchase_Order);

            if (id == null || _context.Purchase_Orders== null)
            {
                return NotFound();
            }

            var purchase_Order = await _context.Purchase_Orders
                            .Include(s => s.vendor)
                            .Include(s => s.payment_methodNavigation)
                            .FirstOrDefaultAsync(m => m.purchase_id== id);
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

            ViewData["customer_id"] = new SelectList(_context.Customers, "customer_id", "customer_id", purchase_Order.vendor_id);
            ViewData["payment_method"] = new SelectList(_context.Payments, "id", "method_name", purchase_Order.payment_method);
            return View(viewModel);
        }

        // POST: Purchase_Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PurchaseViewModel viewModel)
        {
            //if (id != purchase_Order.purchase_id)
            //{
            //    return NotFound();
            //}

            ////if (ModelState.IsValid)
            ////{
            //    try
            //    {
            //        _context.Update(purchase_Order);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!Purchase_OrderExists(purchase_Order.purchase_id))
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
            //ViewData["payment_method"] = new SelectList(_context.Payments, "id", "method_name", purchase_Order.payment_method);
            //ViewData["vendor_id"] = new SelectList(_context.Vendors, "vendor_id", "name", purchase_Order.vendor_id);
            //return View(purchase_Order);

            if (id != viewModel.PurchaseOrder.purchase_id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            try
            {
                _context.Update(viewModel.PurchaseOrder);
                await _context.SaveChangesAsync();
                _context.Update(viewModel.PurchaseItems);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Purchase_OrderExists(viewModel.PurchaseOrder.purchase_id))
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
            ViewData["customer_id"] = new SelectList(_context.Customers, "customer_id", "customer_id", viewModel.PurchaseOrder.vendor_id);
            ViewData["payment_method"] = new SelectList(_context.Payments, "id", "method_name", viewModel.PurchaseOrder.purchase_id);
            return View(viewModel);
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
            // Find the purchase order by ID
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
                return BadRequest("Purchase order is already confirmed.");
            }

            // Update the state to "Sale"
            purchaseOrder.state = "Purchase Order";
            _context.Update(purchaseOrder);
            await _context.SaveChangesAsync();

            // Add items to the transfer table
            var transfer = new Transfer
            {
                Doc_name = "WH-IN/" + DateTime.Now.Date.Year + "/" + DateTime.Now.Date.Month + "/" + DateTime.Now.Date.Day + "/",
                Source_Document = purchaseOrder.doc_name,
                created_date = DateTime.Now.Date,
                status = "Initial"
            };

            _context.Transfers.Add(transfer);
            await _context.SaveChangesAsync();

            // Add details to the transfer detail table
            foreach (var item in purchaseOrder.Purchase_Order_Details)
            {
                var transferDetail = new Transfer_Detail
                {
                    transfer_id = transfer.ID,
                    product_id = item.product_id,
                    demand = item.quantity,
                    done = 0
                };

                _context.Transfer_Details.Add(transferDetail);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //Creates Bill after GRN is Is complete
        public async Task<IActionResult> CreateBill(int id)
        {
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
    }
}
