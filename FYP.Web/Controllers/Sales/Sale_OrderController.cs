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
using FYP.Services;
using FYP.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace FYP.Web.Controllers.Sales
{
    [Authorize(Roles =("Admin,Sale"))]
    public class Sale_OrderController : Controller
    {
        private int count = 0;

        private readonly FYPContext _context;
        private readonly PDF_Generator _pdfGenerator;


        public Sale_OrderController(FYPContext context, PDF_Generator pdfGenerator)
        {
            _context = context;
            _pdfGenerator = pdfGenerator;
        }

        // GET: Sale_Order
        public async Task<IActionResult> Index()
        {
            var fYPContext = _context.Sale_Orders.Include(s => s.customer).Include(s => s.payment_methodNavigation);
            return View(await fYPContext.ToListAsync());
        }

        // GET: Sale_Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           

            if (id == null)
            {
                return NotFound();
            }

            var sale_Order = await _context.Sale_Orders
                .Include(s => s.customer)
                .Include(s => s.payment_methodNavigation)
                .FirstOrDefaultAsync(m => m.sale_id == id);

            if (sale_Order == null)
            {
                return NotFound();
            }

            var viewModel = new SalesViewModel
            {
                SaleOrder = sale_Order,
                SaleItems = await _context.Sale_Order_Details
                .Include(d=>d.product)
            .Where(d => d.sale_id == id)
            .ToListAsync()
            };

            return View(viewModel);
        }

        // GET: Sale_Order/Create

        public IActionResult Create()
        {
            ViewData["customer_id"] = new SelectList(_context.Customers, "customer_id", "customer_name");
            ViewData["payment_method"] = new SelectList(_context.Payments, "id", "method_name");
            ViewData["product_id"] = new SelectList(_context.Products, "product_id", "name");
            var viewModel = new SalesViewModel
            {
                SaleOrder = new DB.DBTables.Sale_Order(),
                SaleItems = new List<DB.DBTables.Sale_Order_Detail>(),
            };

            return View(viewModel);
        }


        // POST: Sale_Order/Create
        //public async Task<IActionResult> Create([Bind("sale_id,customer_id,name,total_amount,payment_method,date_created,state")] Sale_Order sale_Order)
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalesViewModel viewModel)
        {
            //viewModel.SaleOrder.name = string.Format("S{0:D5}", _context.Sale_Orders.Count() + 1);
            string? maxExistingName = _context.Sale_Orders
                                        .Select(so => so.name)
                                        .OrderByDescending(name => name)
                                        .FirstOrDefault();

            // Extract the numeric part and increment it
            int count = 0;
            if (!string.IsNullOrEmpty(maxExistingName) && maxExistingName.Length >= 2)
            {
                if (int.TryParse(maxExistingName.Substring(1), out int numericPart))
                {
                    count = numericPart + 1;
                }
            }

            // Generate the new name
            string newUniqueName = string.Format("S{0:D5}", count);

            // Set the new unique name
            viewModel.SaleOrder.name = newUniqueName;

            // Save the sale order header to the database
            _context.Sale_Orders.Add(viewModel.SaleOrder);
            _context.SaveChanges();

            // Associate the line items with the sale order
            if(viewModel.SaleItems is not null) { 
            foreach (var item in viewModel.SaleItems)
            {
                var lineItem = new DB.DBTables.Sale_Order_Detail
                {
                    sale_id = viewModel.SaleOrder.sale_id,
                    product_id = item.product_id,
                    quantity = Convert.ToInt32(item.quantity),
                    price = item.price
                };
                //if (lineItem.quantity < lineItem.product.quantity)
                //{
                    _context.Sale_Order_Details.Add(lineItem);
                    _context.SaveChanges();
                //}
            }
            }

            // Redirect to a success page or take appropriate action
            return RedirectToAction("Index");
            //}
            return View(viewModel);
        }

        // GET: Sale_Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {

            

                if (id == null || _context.Sale_Orders == null)
                {
                    return NotFound();
                }

                var sale_Order = await _context.Sale_Orders
                                .Include(s => s.customer)
                                .Include(s => s.payment_methodNavigation)
                                .FirstOrDefaultAsync(m => m.sale_id == id); 
                if (sale_Order == null)
                {
                    return NotFound();
                }

                var viewModel = new SalesViewModel
                {
                    SaleOrder = sale_Order,
                    SaleItems = await _context.Sale_Order_Details
                    .Include(d => d.product)
                .Where(d => d.sale_id == id)
                .ToListAsync()
                };
                ViewData["product_id"] = new SelectList(_context.Products, "product_id", "name");

                ViewData["customer_id"] = new SelectList(_context.Customers, "customer_id", "customer_name", sale_Order.customer_id);
                ViewData["payment_method"] = new SelectList(_context.Payments, "id", "method_name", sale_Order.payment_method);

                return View(viewModel);
            }
            catch (Exception e)
            {

                return View("Error", new ErrorViewModel
                {
                    ErrorMessage = "An error occurred while processing your request."
                });
            }

        }

        // POST: Sale_Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("sale_id,customer_id,name,total_amount,payment_method,date_created,state")] Sale_Order sale_Order)
            public async Task<IActionResult> Edit(int id, SalesViewModel viewModel)
        {
            if (id != viewModel.SaleOrder.sale_id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            try
            {
                //if(viewModel.SaleItems is not null) 
                //{ 
                //var existingsaleItems = _context.Sale_Order_Details
                //                .Where(item => item.sale_id == viewModel.SaleOrder.sale_id)
                //                .ToList();
                //foreach (var existingsaleItem in existingsaleItems)
                //{
                //    _context.Sale_Order_Details.Remove(existingsaleItem);
                //}

                //}
                //_context.Update(viewModel.SaleOrder);
                //await _context.SaveChangesAsync();
                //_context.Update(viewModel.SaleItems);
                //await _context.SaveChangesAsync();

                var existingPurchaseItems = _context.Sale_Order_Details
                                .Where(item => item.sale_id == viewModel.SaleOrder.sale_id)
                                .ToList();
                foreach (var existingPurchaseItem in existingPurchaseItems)
                {
                    _context.Sale_Order_Details.Remove(existingPurchaseItem);
                }

                _context.Update(viewModel.SaleOrder);
                await _context.SaveChangesAsync();

                //foreach (var purchase in viewModel.PurchaseItems) 
                //{
                //            purchase.purchase_id = viewModel.PurchaseOrder.purchase_id;
                //            purchase.ID = purchase.ID;
                //            _context.Update(purchase);
                //            _context.SaveChanges();
                //}

                if (viewModel.SaleItems is not null)
                {
                    foreach (var item in viewModel.SaleItems)
                    {
                        var lineItem = new DB.DBTables.Sale_Order_Detail
                        {
                            sale_id = viewModel.SaleOrder.sale_id,
                            product_id = item.product_id,
                            quantity = item.quantity,
                            price = item.price
                        };
                        //if (lineItem.quantity < lineItem.product.quantity)
                        //{
                        _context.Sale_Order_Details.Add(lineItem);
                        _context.SaveChanges();
                        //}
                    }
                }


            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Sale_OrderExists(viewModel.SaleOrder.sale_id))
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
            ViewData["customer_id"] = new SelectList(_context.Customers, "customer_id", "customer_id", viewModel.SaleOrder.customer_id);
            ViewData["payment_method"] = new SelectList(_context.Payments, "id", "method_name", viewModel.SaleOrder.payment_method);
            return View(viewModel);
        }


        // GET: Sale_Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sale_Orders == null)
            {
                return NotFound();
            }

            var sale_Order = await _context.Sale_Orders
                .Include(s => s.customer)
                .Include(s => s.payment_methodNavigation)
                .FirstOrDefaultAsync(m => m.sale_id == id);
            if (sale_Order == null)
            {
                return NotFound();
            }

            return View(sale_Order);
        }

        // POST: Sale_Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sale_Orders == null)
            {
                return Problem("There's nothing in Sale Orders.");
            }
            
            var sale_Order = await _context.Sale_Orders.FindAsync(id);
            if (sale_Order != null)
            {
                _context.Sale_Orders.Remove(sale_Order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Sale_OrderExists(int id)
        {
            return (_context.Sale_Orders?.Any(e => e.sale_id == id)).GetValueOrDefault();
        }


        // POST: SaleOrders/ConfirmSaleOrder/5
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmSaleOrder(int id)
        {
            // Find the sale order by ID
            var saleOrder = await _context.Sale_Orders
                .Include(so => so.Sale_Order_Details)
                .FirstOrDefaultAsync(so => so.sale_id == id);

            if (saleOrder == null)
            {
                return NotFound();
            }

            // Check if the sale order is already confirmed
            if (saleOrder.state == "Sale Order")
            {
                //return BadRequest("Sale order is already confirmed.");
                TempData["SalePopupMessage"] = "Sale order is already confirmed.";
                return RedirectToAction(nameof(Index));
            }

            // Update the state to "Sale"
            saleOrder.state = "Sale Order";
            _context.Update(saleOrder);
            await _context.SaveChangesAsync();

            // Add items to the transfer table
            var transfer = new DB.DBTables.Transfer
            {
                Doc_name = "WH-OUT/" + DateTime.Now.Date.Year + "/" + DateTime.Now.Date.Month + "/" + DateTime.Now.Date.Day + "/" + saleOrder.name,
                Source_Document = saleOrder.name,
                created_date = DateTime.Now.Date,
                status = "Initial",
                operation_type = "DN"
            };

            _context.Transfers.Add(transfer);
            await _context.SaveChangesAsync();

            // Add details to the transfer detail table
            foreach (var item in saleOrder.Sale_Order_Details)
            {
                var transferDetail = new DB.DBTables.Transfer_Detail
                {
                    transfer_id = transfer.ID,
                    product_id = item.product_id,
                    demand = item.quantity,
                    done = 0
                };

                _context.Transfer_Details.Add(transferDetail);
            }

            await _context.SaveChangesAsync();
            TempData["SalePopupMessage"] = "Sale Order confirmed.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInvoice(int id)
        {
            // Find the sale order by ID
            var saleOrder = await _context.Sale_Orders
                .FirstOrDefaultAsync(so => so.sale_id == id);


            if (saleOrder == null)
            {
                return NotFound();
            }

            // Check if the sale order is already confirmed
            if (saleOrder.state == "Sale Order")
            {

                var transfer = await _context.Transfers
                .FirstOrDefaultAsync(t => t.Source_Document == saleOrder.name && t.status == "Done");
                //if transfer is done for that sale order

                if (transfer != null)
                {
                    if (transfer.status == "Done")
                    {
                        var invoice = new DB.DBTables.Account_Move
                        {
                            Doc_Name = "INV/" + DateTime.Now.Date.Year + "/" + DateTime.Now.Date.Month + "/" + DateTime.Now.Date.Day + "/" + (++count),
                            Source_Doc = saleOrder.name,
                            Date_Created = DateTime.Now.Date,
                            Status = "Draft",
                            operation_type = "Invoice"
                        };

                        _context.Add(invoice);
                        await _context.SaveChangesAsync();
                        // Add details to the transfer detail table
                        foreach (var item in saleOrder.Sale_Order_Details)
                        {
                            var invoicelines = new DB.DBTables.Invoice_line
                            {
                                account_id = invoice.ID,
                                product_id = item.product_id,
                                qty = item.quantity,
                                price = item.price,
                                taxes = .18
                            };

                            _context.Invoice_lines.Add(invoicelines);
                        }
                    }
                    else
                    {
                        //if transfer is not
                        TempData["TransferPopupMessage"] = "Invoice cannot be created when Goods are not delivered.";
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


        public async Task<IActionResult> GeneratePDF(int id)
        {
            var sale_Order = await  _context.Sale_Orders
                .Include(so=>so.customer)
                .FirstOrDefaultAsync(so=>so.sale_id==id);
            string data = 
                "<h1>"+sale_Order.name+"<h1/>"
                +"<div class='row'>" +
                    "<div class='col-6'>" +
                    "<div class='row'><span>Date Created: </span><span>"+sale_Order.date_created.Value.ToString("dd MMMM yyyy")+ "</span></div>  " +
                    "<div class='row'><span>Date Printed: </span><span>"+DateTime.Now.ToString("dd MMMM yyyy")+"</span></div>" +
                    "</div>" +
                    "<div class='col-6>" +
                    "<div class='row'><span>Customer: </span><span>"+sale_Order.customer.customer_name+ "</span></div>"
                    +
                    "</div>"+
                "</div>";
            var pdfBytes = _pdfGenerator.GetHTMLPageAsPDF(data);

            // Set the response headers
            Response.Headers.Add("Content-Length", pdfBytes.Length.ToString());
            Response.Headers.Add("Content-Disposition", "inline; filename=Document_" + sale_Order.name + ".pdf");

            return File(pdfBytes, "application/pdf");
        }
    }
}
