using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Index("account_id", Name = "IX_Invoice_lines_account_id")]
public partial class Invoice_line
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "The Product ID field is required.")]
    public int product_id { get; set; }

    [Required(ErrorMessage = "The Quantity field is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive integer.")]
    public int qty { get; set; }

    [Required(ErrorMessage = "The Price field is required.")]
    [Column(TypeName = "money")]
    [DataType(DataType.Currency)]
	[DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
	public decimal price { get; set; }

    [Required(ErrorMessage = "The Taxes field is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "Taxes must be a non-negative number.")]
	[DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]

	public double taxes { get; set; }

    [Required(ErrorMessage = "The Account ID field is required.")]
    public int account_id { get; set; }

    [ForeignKey("account_id")]
    [InverseProperty("Invoice_lines")]
    public virtual Account_Move account { get; set; } = null!;
}
