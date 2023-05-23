using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Table("Sale_Order")]
[Index("Name", Name = "IX_Sale_Order_1", IsUnique = true)]
public partial class SaleOrder
{
    [Key]
    [Column("sale_id")]
    public int SaleId { get; set; }

    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Column("name")]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("total_amount", TypeName = "money")]
    public decimal TotalAmount { get; set; }

    [Column("payment_method")]
    public int PaymentMethod { get; set; }

    [Column("date_created", TypeName = "date")]
    public DateTime? DateCreated { get; set; }

    [Column("state")]
    [StringLength(50)]
    [Unicode(false)]
    public string? State { get; set; }

    public virtual ICollection<AccountMove> AccountMoves { get; set; } = new List<AccountMove>();

    [ForeignKey("CustomerId")]
    [InverseProperty("SaleOrders")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("PaymentMethod")]
    [InverseProperty("SaleOrders")]
    public virtual Payment PaymentMethodNavigation { get; set; } = null!;

    [InverseProperty("Sale")]
    public virtual ICollection<SaleOrderDetail> SaleOrderDetails { get; set; } = new List<SaleOrderDetail>();
}
