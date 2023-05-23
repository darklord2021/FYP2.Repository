using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Table("Account_Move")]
public partial class Account_Move
{
    [Key]
    public int ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Doc_Name { get; set; }

    [Column(TypeName = "money")]
    public decimal? Total_Amount { get; set; }

    [Column(TypeName = "date")]
    public DateTime? Date_Created { get; set; }

    public double? Taxed_Amount { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Source_Doc { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Status { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? operation_type { get; set; }

    public double? tax { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? purchase_source_doc { get; set; }

    [InverseProperty("accountNavigation")]
    public virtual ICollection<Account_Journal> Account_Journals { get; set; } = new List<Account_Journal>();

    [InverseProperty("account")]
    public virtual ICollection<Invoice_line> Invoice_lines { get; set; } = new List<Invoice_line>();

    public virtual Sale_Order? Source_DocNavigation { get; set; }

    public virtual Purchase_Order? purchase_source_docNavigation { get; set; }
}
