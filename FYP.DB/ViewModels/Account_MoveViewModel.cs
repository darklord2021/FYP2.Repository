using FYP.DB.DBTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.DB.ViewModels
{
    public class Account_MoveViewModel
    {
        public Account_Move Account { get; set; }
        public List<Invoice_line> Invoice_Details { get; set; }
    }
}
