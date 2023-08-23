using FYP.DB.Context;
using FYP.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


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
            var salesData = _context.Sale_Orders
                    .Include(s => s.payment_methodNavigation) // Include the payment_methodNavigation navigation property
                    .ToList(); // Fetch sales data from the database
            var groupedSales = salesData
            .GroupBy(s => new { Month = s.date_created.Value.Month, Year = s.date_created.Value.Month})
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


            int totalSales = _context.Sale_Orders.Count(s => s.state == "Sale Order");
            ViewBag.TotalSales = totalSales;

            decimal revenue = _context.Sale_Orders.Where(s => s.state == "Sale Order").Sum(s=>s.total_amount);
            ViewBag.TotalSalesrevenue = revenue;

            int totalQuotations = _context.Sale_Orders.Count(s => s.state == "Quotation");
            ViewBag.TotalQuotations = totalQuotations;

            int totalOrders= _context.Sale_Orders.Count();
            ViewBag.TotalOrders= totalOrders;
            // Pass the total sales value to the view
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