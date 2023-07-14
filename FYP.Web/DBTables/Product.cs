using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.Web.DBTables;

[Table("Product")]
public partial class Product
{
    [Key]
    public int product_id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string name { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? description { get; set; }

    public int? quantity { get; set; }

    [Column(TypeName = "money")]
    public decimal? unit_price { get; set; }

    public int category_id { get; set; }

    public int? reorder_level { get; set; }

    public bool discontinued { get; set; }

    [InverseProperty("product")]
    public virtual ICollection<Purchase_Order_Detail> Purchase_Order_Details { get; set; } = new List<Purchase_Order_Detail>();

    [InverseProperty("product")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("product")]
    public virtual ICollection<Sale_Order_Detail> Sale_Order_Details { get; set; } = new List<Sale_Order_Detail>();

    [InverseProperty("product")]
    public virtual ICollection<Transfer_Detail> Transfer_Details { get; set; } = new List<Transfer_Detail>();

    [ForeignKey("category_id")]
    [InverseProperty("Products")]
    public virtual Category category { get; set; } = null!;
}
