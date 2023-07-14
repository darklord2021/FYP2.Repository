using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.Web.DBTables;

[Table("Purchase_Order")]
[Index("doc_name", Name = "IX_Purchase_Order", IsUnique = true)]
public partial class Purchase_Order
{
    [Key]
    public int purchase_id { get; set; }

    public int vendor_id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string doc_name { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal cost { get; set; }

    [Column(TypeName = "date")]
    public DateTime? create_date { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string state { get; set; } = null!;

    public int payment_method { get; set; }

    public virtual ICollection<Account_Move> Account_Moves { get; set; } = new List<Account_Move>();

    [InverseProperty("purchase")]
    public virtual ICollection<Purchase_Order_Detail> Purchase_Order_Details { get; set; } = new List<Purchase_Order_Detail>();

    [ForeignKey("payment_method")]
    [InverseProperty("Purchase_Orders")]
    public virtual Payment payment_methodNavigation { get; set; } = null!;

    [ForeignKey("vendor_id")]
    [InverseProperty("Purchase_Orders")]
    public virtual Vendor vendor { get; set; } = null!;
}
