using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

public partial class Review
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("stars")]
    public byte? Stars { get; set; }

    [Column(TypeName = "text")]
    public string Description { get; set; } = null!;

    [Column("customer_id")]
    public int? CustomerId { get; set; }

    [Column("product_id")]
    public int? ProductId { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Reviews")]
    public virtual Customer? Customer { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Reviews")]
    public virtual Product? Product { get; set; }
}
