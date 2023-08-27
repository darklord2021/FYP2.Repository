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
    [Display(Name = "Sale Order Reference")]
    public int sale_id { get; set; }

    [Required(ErrorMessage = "The Product ID field is required.")]
    public int product_id { get; set; }

    [Display(Name = "Quantity")]
    [Required(ErrorMessage = "The Quantity field is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive integer.")]
    public int quantity { get; set; }
    [Display(Name = "Unit Price")]
    [Required(ErrorMessage = "The Price field is required.")]
    [Column(TypeName = "money")]
    [Range(0,double.MaxValue,ErrorMessage ="Price Cannot be negative")]
    [DataType(DataType.Currency)]
	[DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
	public decimal price { get; set; }

    [ForeignKey("product_id")]
    [InverseProperty("Sale_Order_Details")]
    [Display(Name = "Product")]
    public virtual Product product { get; set; } = null!;

    [ForeignKey("sale_id")]
    [InverseProperty("Sale_Order_Details")]
    [Display(Name = "Sale Order Reference")]
    public virtual Sale_Order sale { get; set; } = null!;
}
