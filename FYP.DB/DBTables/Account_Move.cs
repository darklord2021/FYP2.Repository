using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FYP.DB.DBTables;

public partial class Account_Move
{
    public int ID { get; set; }
    [Display(Name = "Document Name")]
    public string? Doc_Name { get; set; }
    [Display(Name = "Total Amount")]
    public decimal? Total_Amount { get; set; }
    [Display(Name = "Created on")]
    public DateTime? Date_Created { get; set; }
    [Display(Name = "Taxed Amount")]
    public double? Taxed_Amount { get; set; }
    [Display(Name = "Sales Reference")]
    public string? Source_Doc { get; set; }
    [Display(Name = "Status")]
    public string? Status { get; set; }
    [Display(Name = "Operation Type")]
    public string? operation_type { get; set; }
    [Display(Name = "Tax")]
    public double? tax { get; set; }
    [Display(Name = "Payment Status")]
    public bool paid { get; set; }
    [Display(Name = "Purchase Reference")]
    public string? purchase_source_doc { get; set; }

    public virtual ICollection<Invoice_line> Invoice_lines { get; set; } = new List<Invoice_line>();

    public virtual Sale_Order? Source_DocNavigation { get; set; }

    public virtual Purchase_Order? purchase_source_docNavigation { get; set; }
}
