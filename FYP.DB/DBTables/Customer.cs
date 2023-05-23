using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

public partial class Customer
{
    [Key]
    public int customer_id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? customer_name { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? email { get; set; }

    public long? phone { get; set; }

    [Column(TypeName = "text")]
    public string? address { get; set; }

    public double? record { get; set; }

    [InverseProperty("customer")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("customer")]
    public virtual ICollection<Sale_Order> Sale_Orders { get; set; } = new List<Sale_Order>();
}
