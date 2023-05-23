using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Table("Account_Move")]
public partial class AccountMove
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("Doc_Name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? DocName { get; set; }

    [Column("Total_Amount", TypeName = "money")]
    public decimal? TotalAmount { get; set; }

    [Column("Date_Created", TypeName = "date")]
    public DateTime? DateCreated { get; set; }

    [Column("Taxed_Amount")]
    public double? TaxedAmount { get; set; }

    [Column("Source_Doc")]
    [StringLength(50)]
    [Unicode(false)]
    public string? SourceDoc { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Status { get; set; }

    [Column("operation_type")]
    [StringLength(50)]
    [Unicode(false)]
    public string? OperationType { get; set; }

    [Column("tax")]
    public double? Tax { get; set; }

    [Column("purchase_source_doc")]
    [StringLength(50)]
    [Unicode(false)]
    public string? PurchaseSourceDoc { get; set; }

    [InverseProperty("AccountNavigation")]
    public virtual ICollection<AccountJournal> AccountJournals { get; set; } = new List<AccountJournal>();

    [InverseProperty("Account")]
    public virtual ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();

    public virtual PurchaseOrder? PurchaseSourceDocNavigation { get; set; }

    public virtual SaleOrder? SourceDocNavigation { get; set; }
}
