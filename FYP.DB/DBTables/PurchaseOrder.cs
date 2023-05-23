using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Table("Purchase_Order")]
[Index("DocName", Name = "IX_Purchase_Order", IsUnique = true)]
public partial class PurchaseOrder
{
    [Key]
    [Column("purchase_id")]
    public int PurchaseId { get; set; }

    [Column("vendor_id")]
    public int VendorId { get; set; }

    [Column("doc_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string DocName { get; set; } = null!;

    [Column("cost", TypeName = "money")]
    public decimal Cost { get; set; }

    [Column("create_date", TypeName = "date")]
    public DateTime? CreateDate { get; set; }

    [Column("state")]
    [StringLength(50)]
    [Unicode(false)]
    public string State { get; set; } = null!;

    [Column("payment_method")]
    public int PaymentMethod { get; set; }

    public virtual ICollection<AccountMove> AccountMoves { get; set; } = new List<AccountMove>();

    [ForeignKey("PaymentMethod")]
    [InverseProperty("PurchaseOrders")]
    public virtual Payment PaymentMethodNavigation { get; set; } = null!;

    [InverseProperty("Purchase")]
    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();

    [ForeignKey("VendorId")]
    [InverseProperty("PurchaseOrders")]
    public virtual Vendor Vendor { get; set; } = null!;
}
