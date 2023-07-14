using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FYP.DB.DBTables;

public partial class Purchase_Order_Detail
{
    public int ID { get; set; }
    [Display(Name ="Purchase ID")]
    public int? purchase_id { get; set; }
    [Display(Name = "Product")]
    public int? product_id { get; set; }
    [Display(Name = "Quantity")]
    public int? quantity { get; set; }
    [Display(Name = "Price")]
    public decimal? price { get; set; }

    public virtual Product? product { get; set; }

    public virtual Purchase_Order? purchase { get; set; }
}
