using FYP.DB.Context;
using FYP.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FYP.Web.Controllers
{
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