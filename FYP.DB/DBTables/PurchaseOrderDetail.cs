using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Table("Purchase_Order_Details")]
public partial class PurchaseOrderDetail
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("purchase_id")]
    public int? PurchaseId { get; set; }

    [Column("product_id")]
    public int? ProductId { get; set; }

    [Column("quantity")]
    public int? Quantity { get; set; }

    [Column("price", TypeName = "money")]
    public decimal? Price { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("PurchaseOrderDetails")]
    public virtual Product? Product { get; set; }

    [ForeignKey("PurchaseId")]
    [InverseProperty("PurchaseOrderDetails")]
    public virtual PurchaseOrder? Purchase { get; set; }
}
