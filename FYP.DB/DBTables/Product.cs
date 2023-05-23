using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Table("Product")]
public partial class Product
{
    [Key]
    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("name")]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Column("quantity")]
    public int? Quantity { get; set; }

    [Column("unit_price", TypeName = "money")]
    public decimal? UnitPrice { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("reorder_level")]
    public int? ReorderLevel { get; set; }

    [Column("discontinued")]
    public bool Discontinued { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; } = null!;

    [InverseProperty("Product")]
    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();

    [InverseProperty("Product")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("Product")]
    public virtual ICollection<SaleOrderDetail> SaleOrderDetails { get; set; } = new List<SaleOrderDetail>();

    [InverseProperty("Product")]
    public virtual ICollection<TransferDetail> TransferDetails { get; set; } = new List<TransferDetail>();
}
