using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FYP.DB.DBTables;

public partial class Product
{
    public int product_id { get; set; }
    [Display(Name = "Document Name")]
    public string name { get; set; } = null!;
    [Display(Name = "Description")]
    public string? description { get; set; }
    [Display(Name = "Quantity")]
    public int? quantity { get; set; }
    [Display(Name = "Unit Price")]
    public decimal? unit_price { get; set; }
    [Display(Name = "Category")]
    public int category_id { get; set; }
    [Display(Name = "Reorder Level")]
    public int? reorder_level { get; set; }
    [Display(Name = "Discontinued")]
    public bool discontinued { get; set; }

    public virtual ICollection<Purchase_Order_Detail> Purchase_Order_Details { get; set; } = new List<Purchase_Order_Detail>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Sale_Order_Detail> Sale_Order_Details { get; set; } = new List<Sale_Order_Detail>();

    public virtual ICollection<Transfer_Detail> Transfer_Details { get; set; } = new List<Transfer_Detail>();
    [Display(Name = "Category")]
    public virtual Category category { get; set; } = null!;
}
