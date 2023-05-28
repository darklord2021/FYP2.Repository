using FYP.DB.DBTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.DB.ViewModels
{
    public class PurchaseViewModel
    {
        public Purchase_Order PurchaseOrder { get; set; }
        public List<Purchase_Order_Detail> PurchaseItems { get; set; }
    }
}
