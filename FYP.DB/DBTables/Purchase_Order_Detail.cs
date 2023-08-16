using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Index("product_id", Name = "IX_Purchase_Order_Details_product_id")]
[Index("purchase_id", Name = "IX_Purchase_Order_Details_purchase_id")]
public partial class Purchase_Order_Detail
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "The Purchase ID field is required.")]
    public int? purchase_id { get; set; }

    [Required(ErrorMessage = "The Product ID field is required.")]
    public int? product_id { get; set; }

    [Required(ErrorMessage = "The Quantity field is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive integer.")]
    public int? quantity { get; set; }

    [Required(ErrorMessage = "The Price field is required.")]
    [Column(TypeName = "money")]
    [DataType(DataType.Currency)]
    public decimal? price { get; set; }

    [ForeignKey("product_id")]
    [InverseProperty("Purchase_Order_Details")]
    public virtual Product? product { get; set; }

    [ForeignKey("purchase_id")]
    [InverseProperty("Purchase_Order_Details")]
    public virtual Purchase_Order? purchase { get; set; }
}
