using FYP.DB.DBTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.DB.ViewModels
{
    public class SalesViewModel
    {
        public Sale_Order SaleOrder { get; set; }
        public List<Sale_Order_Detail> SaleItems { get; set; }
    }
}
