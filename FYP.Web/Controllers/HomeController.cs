using FYP.DB.Context;
using FYP.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
//using System.Web.Helpers;

namespace FYP.Web.Controllers
{
    [Authorize(Roles ="Admin,Sale,Purchase,Inventory,Finance")]
    
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly FYPContext _context;
        public HomeController(ILogger<HomeController> logger,FYPContext context)
        {
            _context = context;
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            var topsoldproducts=_context.Sale_Order_Details
                .Include(s=>s.product)
                .GroupBy(s=>s.product.name)
                .Select(group=> new TopSoldProductViewModel
                {
                    ProductName= group.Key,
                    TotalRevenue = group.Sum(s => s.quantity * s.price),
                    Quantity=group.Sum(s => s.quantity)
                }).OrderByDescending(s=>s.TotalRevenue)
                .ToList();
            
            ViewBag.MostSoldItems = topsoldproducts;

            var toppurchasedproducts = _context.Purchase_Order_Details
                .Include(s => s.product)
                .GroupBy(s => s.product.name)
                .Select(group => new TopPurchasedProductViewModel
                {
                    ProductName = group.Key,
                    TotalCost = group.Sum(s => s.quantity * s.price),
                    Quantity = group.Sum(s => s.quantity)
                }).OrderByDescending(s => s.TotalCost)
                .ToList();

            ViewBag.MostPurchasedItems = toppurchasedproducts;

            var payingcustomers = _context.Sale_Order_Details.Include(s => s.sale).Include(c => c.sale.customer).GroupBy(s => s.sale.customer.customer_name)
                .Select(group => new TopCustomersViewModel
                {
                    Customers = group.Key,
                    amount_spent = group.Sum(s => s.quantity * s.price),
                    quantity_purchased = group.Sum(s => s.quantity)

                }).OrderByDescending(a=>a.amount_spent)
                .ToList();
            ViewBag.TopCustomers= payingcustomers;
            var salesData = _context.Sale_Orders
                    .Include(s => s.payment_methodNavigation) // Include the payment_methodNavigation navigation property
                    .ToList(); // Fetch sales data from the database

            var monthData = salesData.Select(c => c.date_created).ToList();
            foreach(var item in monthData)
            {
                Console.WriteLine(item.Value.ToString());
            }
            var temp = salesData
            .GroupBy(s => new { Month = s.date_created.Value.Month, Year = s.date_created.Value.Year }).ToList();

            var temp2 = temp.Select(c => new { MonthYear = c.Key.Month, Year = c.Key.Year }).ToList();


            var groupedSales = salesData
            .GroupBy(s => new { Month = s.date_created.Value.Month, Year = s.date_created.Value.Year})
            .Select(group => new
            {
                MonthYear = new DateTime(group.Key.Year, group.Key.Month, 1),
                OfflineSales = group.Where(s => s.payment_methodNavigation.method_name =="Cash").Sum(s => s.total_amount),
                OnlineSales = group.Where(s => s.payment_methodNavigation.method_name != "Cash").Sum(s => s.total_amount)
            })
            .OrderBy(item => item.MonthYear)
            .ToList();

            var months = groupedSales.Select(item => item.MonthYear).ToList();
            var offlineSales = groupedSales.Select(item => item.OfflineSales).ToList();
            var onlineSales = groupedSales.Select(item => item.OnlineSales).ToList();

            ViewBag.Months = months; ViewBag.OnlineSales = onlineSales;
            ViewBag.OfflineSales = offlineSales;

            int totalSales = _context.Sale_Orders.Count(s => s.state == "Sale Order");
            ViewBag.TotalSales = totalSales.ToString("N2");

            decimal revenue = _context.Sale_Orders.Where(s => s.state == "Sale Order").Sum(s=>s.total_amount);
            ViewBag.TotalSalesrevenue = revenue.ToString("N2");

            int totalQuotations = _context.Sale_Orders.Count(s => s.state == "Quotation");
            ViewBag.TotalQuotations = totalQuotations.ToString("N2");

            int totalOrders= _context.Sale_Orders.Count();
            ViewBag.TotalOrders= totalOrders.ToString("N2");

            
            // Pass the total sales value to the view
            _logger.Log(LogLevel.Information,"At Dashboard");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }

    
}