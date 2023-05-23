using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Table("Payment")]
public partial class Payment
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("method_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string MethodName { get; set; } = null!;

    [InverseProperty("PaymentMethodNavigation")]
    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    [InverseProperty("PaymentMethodNavigation")]
    public virtual ICollection<SaleOrder> SaleOrders { get; set; } = new List<SaleOrder>();
}
