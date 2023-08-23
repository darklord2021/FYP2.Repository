using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Table("Account_Move")]
[Index("Source_Doc", Name = "IX_Account_Move_Source_Doc")]
[Index("purchase_source_doc", Name = "IX_Account_Move_purchase_source_doc")]
[Display(Name ="Account Move")]
public partial class Account_Move
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "The Document Name is required.")]
    [StringLength(50, ErrorMessage = "The Document Name must be at most {1} characters long.")]
    [Display(Name = "Document Name")]
    [Unicode(false)]
    public string? Doc_Name { get; set; }

    [Column(TypeName = "money")]
    [Display(Name = "Total Amount")]
    public decimal? Total_Amount { get; set; }

    [Column(TypeName = "date")]
    [Display(Name = "Date Created")]
    public DateTime? Date_Created { get; set; }

    [Display(Name = "Taxed Amount")]
    public double? Taxed_Amount { get; set; }

    [StringLength(50)]
    [Display(Name = "Source Document")]
    [Unicode(false)]
    public string? Source_Doc { get; set; }

    [StringLength(50)]
    [Display(Name = "Status")]
    [Unicode(false)]
    public string? Status { get; set; }

    [StringLength(50)]
    [Display(Name = "Operation Type")]
    [Unicode(false)]
    public string? operation_type { get; set; }

    [Display(Name = "Tax")]
    public double? tax { get; set; }

    [StringLength(50)]
    [Display(Name = "Purchase Source Document")]
    [Unicode(false)]
    public string? purchase_source_doc { get; set; }

    [Required(ErrorMessage = "The 'paid' field is required.")]
    public bool? paid { get; set; }

    [InverseProperty("account")]
    public virtual ICollection<Invoice_line> Invoice_lines { get; set; } = new List<Invoice_line>();

    [ForeignKey("Source_Doc")]
    [InverseProperty("Account_Moves")]
    [Display(Name ="Sales Source Document")]
    public virtual Sale_Order? Source_DocNavigation { get; set; }

    [ForeignKey("purchase_source_doc")]
    [InverseProperty("Account_Moves")]
    [Display(Name = "Purchase Source Document")]

    public virtual Purchase_Order? purchase_source_docNavigation { get; set; }
}
