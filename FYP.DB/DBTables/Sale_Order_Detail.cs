using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Index("product_id", Name = "IX_Sale_Order_Details_product_id")]
[Index("sale_id", Name = "IX_Sale_Order_Details_sale_id")]
public partial class Sale_Order_Detail
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "The Sale ID field is required.")]
    public int sale_id { get; set; }

    [Required(ErrorMessage = "The Product ID field is required.")]
    public int product_id { get; set; }

    [Required(ErrorMessage = "The Quantity field is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive integer.")]
    public int quantity { get; set; }

    [Required(ErrorMessage = "The Price field is required.")]
    [Column(TypeName = "money")]
    [DataType(DataType.Currency)]
    public decimal price { get; set; }

    [ForeignKey("product_id")]
    [InverseProperty("Sale_Order_Details")]
    public virtual Product product { get; set; } = null!;

    [ForeignKey("sale_id")]
    [InverseProperty("Sale_Order_Details")]
    public virtual Sale_Order sale { get; set; } = null!;
}
