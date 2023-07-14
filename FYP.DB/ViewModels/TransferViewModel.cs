using FYP.DB.DBTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.DB.ViewModels
{
    public class TransferViewModel
    {
        public Transfer Transfer { get; set; }
        public List<Transfer_Detail> LineItems { get; set;}
    }
}
