using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FYP.DB.DBTables;

public partial class Transfer
{
    [Display(Name = "ID")]
    public int ID { get; set; }
    [Required]
    [Display(Name = "Document Name")]
    public string? Doc_name { get; set; }
    [Required]
    [Display(Name = "Source Document")]
    public string? Source_Document { get; set; }
    [Required]
    [Display(Name = "Created on")]
    public DateTime? created_date { get; set; }
    [Display(Name = "Backorder of")]
    public int? backorder_doc_id { get; set; }
    [Required]
    [Display(Name = "Status")]
    public string status { get; set; } = null!;
    [Required]
    [Display(Name = "Operation Type")]
    public string? operation_type { get; set; }

    public virtual ICollection<Transfer> Inversebackorder_doc { get; set; } = new List<Transfer>();

    public virtual ICollection<Transfer_Detail> Transfer_Details { get; set; } = new List<Transfer_Detail>();
    [Display(Name = "Backorder of")]
    public virtual Transfer? backorder_doc { get; set; }
}
