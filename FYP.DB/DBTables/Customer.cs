using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

public partial class Customer
{
    [Key]
    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Column("customer_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? CustomerName { get; set; }

    [Column("email")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("phone")]
    public long? Phone { get; set; }

    [Column("address", TypeName = "text")]
    public string? Address { get; set; }

    [Column("record")]
    public double? Record { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("Customer")]
    public virtual ICollection<SaleOrder> SaleOrders { get; set; } = new List<SaleOrder>();
}
