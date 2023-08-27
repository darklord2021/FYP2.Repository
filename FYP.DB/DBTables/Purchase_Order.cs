using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Table("Purchase_Order")]
[Index("doc_name", Name = "AK_Purchase_Order_doc_name", IsUnique = true)]
[Index("doc_name", Name = "IX_Purchase_Order", IsUnique = true)]
[Index("payment_method", Name = "IX_Purchase_Order_payment_method")]
[Index("vendor_id", Name = "IX_Purchase_Order_vendor_id")]
[Display(Name = "Purchase Order")]
public partial class Purchase_Order
{
    [Key]
    public int purchase_id { get; set; }

    [Required(ErrorMessage = "The Vendor ID field is required.")]
    [Display(Name = "Vendor")]
    public int vendor_id { get; set; }

    [Required(ErrorMessage = "The Document Name field is required.")]
    [StringLength(50)]
    [Display(Name = "Document Name")]
    [Unicode(false)]
    public string doc_name { get; set; } = null!;

    [Required(ErrorMessage = "The Cost field is required.")]
    [Column(TypeName = "money")]
    [DataType(DataType.Currency)]
    [Display(Name = "Cost")]
    public decimal cost { get; set; }

    [Required(ErrorMessage = "The Create Date field is required.")]
    [Column(TypeName = "date")]
    [DataType(DataType.Date)]
    [Display(Name = "Date Created")]
    public DateTime? create_date { get; set; }

    [Required(ErrorMessage = "The State field is required.")]
    [StringLength(50)]
    [Display(Name = "State")]
    [Unicode(false)]
    public string state { get; set; } = null!;

    [Required(ErrorMessage = "The Payment Method field is required.")]
    [Display(Name = "Payment Method")]
    public int payment_method { get; set; }

    public virtual ICollection<Account_Move> Account_Moves { get; set; } = new List<Account_Move>();

    [InverseProperty("purchase")]
    public virtual ICollection<Purchase_Order_Detail> Purchase_Order_Details { get; set; } = new List<Purchase_Order_Detail>();

    [ForeignKey("payment_method")]
    [InverseProperty("Purchase_Orders")]
    [Display(Name ="Payment Method")]
    public virtual Payment payment_methodNavigation { get; set; } = null!;

    [ForeignKey("vendor_id")]
    [InverseProperty("Purchase_Orders")]
    [Display(Name ="Vendor")]
    public virtual Vendor vendor { get; set; } = null!;

	[NotMapped]
	public string FormattedOrderNumber => $"P{purchase_id:D5}";
}
