using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Index("product_id", Name = "IX_Transfer_Details_product_id")]
[Index("transfer_id", Name = "IX_Transfer_Details_transfer_id")]
public partial class Transfer_Detail
{
    [Key]
    public int id { get; set; }

    [Required(ErrorMessage = "The Transfer ID field is required.")]
    public int transfer_id { get; set; }

    [Required(ErrorMessage = "The Product ID field is required.")]
    public int product_id { get; set; }

    [Required(ErrorMessage = "The Demand field is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "Demand must be a non-negative integer.")]
    public int demand { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Done must be a non-negative integer.")]
    public int? done { get; set; }

    [ForeignKey("product_id")]
    [InverseProperty("Transfer_Details")]
    public virtual Product product { get; set; } = null!;

    [ForeignKey("transfer_id")]
    [InverseProperty("Transfer_Details")]
    public virtual Transfer transfer { get; set; } = null!;
}
