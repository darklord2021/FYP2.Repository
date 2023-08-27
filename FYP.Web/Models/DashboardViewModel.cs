namespace FYP.Web.Models
{
    public class DashboardViewModel
    {
        public List<TopSoldProductViewModel> MostSoldItems { get; set; }

    }

    public class TopSoldProductViewModel
    {
        public string ProductName { get; set; }
        public decimal TotalRevenue { get; set; }
        public int Quantity { get;set; }
    }

    public class TopCustomersViewModel
    {
        public string Customers { get; set; }
        public decimal amount_spent { get; set; }
        public int quantity_purchased { get; set; }
    }
    public class TopPurchasedProductViewModel
    {
        public string ProductName { get; set; }
        public decimal? TotalCost { get; set; }
        public int? Quantity { get; set; }
    }
}
