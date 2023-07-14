using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.Web.DBTables;

public partial class Review
{
    [Key]
    public int id { get; set; }

    public byte? stars { get; set; }

    [Column(TypeName = "text")]
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
