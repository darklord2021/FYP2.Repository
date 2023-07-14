using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.Web.DBTables;

[Table("Sale_Order")]
[Index("name", Name = "IX_Sale_Order_1", IsUnique = true)]
public partial class Sale_Order
{
    [Key]
    public int sale_id { get; set; }

    public int customer_id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string name { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal total_amount { get; set; }

    public int payment_method { get; set; }

    [Column(TypeName = "date")]
    public DateTime? date_created { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? state { get; set; }

    public virtual ICollection<Account_Move> Account_Moves { get; set; } = new List<Account_Move>();

    [InverseProperty("sale")]
    public virtual ICollection<Sale_Order_Detail> Sale_Order_Details { get; set; } = new List<Sale_Order_Detail>();

    [ForeignKey("customer_id")]
    [InverseProperty("Sale_Orders")]
    public virtual Customer customer { get; set; } = null!;

    [ForeignKey("payment_method")]
    [InverseProperty("Sale_Orders")]
    public virtual Payment payment_methodNavigation { get; set; } = null!;
}
