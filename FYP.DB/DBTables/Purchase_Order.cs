using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FYP.DB.DBTables;

public partial class Purchase_Order
{
    public int purchase_id { get; set; }

    [Display(Name ="Vendor")]
    public int vendor_id { get; set; }
    [Display(Name = "Document Name")]
    public string doc_name { get; set; } = null!;
    [Display(Name = "Cost")]
    [DataType(DataType.Currency)]
    public decimal cost { get; set; }
    [Display(Name = "Created on")]
    public DateTime? create_date { get; set; }
    [Display(Name = "Status")]
    public string state { get; set; } = null!;
    [Display(Name = "Payment Method")]
    public int payment_method { get; set; }

    public virtual ICollection<Account_Move> Account_Moves { get; set; } = new List<Account_Move>();

    public virtual ICollection<Purchase_Order_Detail> Purchase_Order_Details { get; set; } = new List<Purchase_Order_Detail>();
    [Display(Name = "Payment Method")]
    public virtual Payment payment_methodNavigation { get; set; } = null!;
    [Display(Name = "Vendor")]
    public virtual Vendor vendor { get; set; } = null!;
}
