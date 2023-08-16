using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Index("customer_id", Name = "IX_Reviews_customer_id")]
[Index("product_id", Name = "IX_Reviews_product_id")]
public partial class Review
{
    [Key]
    public int id { get; set; }

    [Range(1, 5, ErrorMessage = "Stars must be a value between 1 and 5.")]
    public byte? stars { get; set; }

    [Required(ErrorMessage = "The Description field is required.")]
    [Column(TypeName = "text")]
    [DataType(DataType.MultilineText)]
    public string Description { get; set; } = null!;

    public int? customer_id { get; set; }

    public int? product_id { get; set; }

    [ForeignKey("customer_id")]
    [InverseProperty("Reviews")]
    public virtual Customer? customer { get; set; }

    [ForeignKey("product_id")]
    [InverseProperty("Reviews")]
    public virtual Product? product { get; set; }
}
