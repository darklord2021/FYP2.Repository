using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FYP.DB.DBTables;

public partial class Invoice_line
{
    public int ID { get; set; }
    [Display(Name = "Product")]
    public int product_id { get; set; }
    [Display(Name = "Quantity")]
    public int qty { get; set; }
    [Display(Name = "Unit Price")]
    public decimal price { get; set; }
    [Display(Name = "Taxes")]
    public double taxes { get; set; }
    [Display(Name = "Account")]
    public int account_id { get; set; }

    public virtual Account_Move account { get; set; } = null!;
}
