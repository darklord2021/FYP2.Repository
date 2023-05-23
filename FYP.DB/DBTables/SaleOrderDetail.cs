using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Table("Sale_Order_Details")]
public partial class SaleOrderDetail
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("sale_id")]
    public int SaleId { get; set; }

    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("price", TypeName = "money")]
    public decimal Price { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("SaleOrderDetails")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("SaleId")]
    [InverseProperty("SaleOrderDetails")]
    public virtual SaleOrder Sale { get; set; } = null!;
}
