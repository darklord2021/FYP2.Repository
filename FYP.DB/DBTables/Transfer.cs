using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Index("backorder_doc_id", Name = "IX_Transfers_backorder_doc_id")]
public partial class Transfer
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "The Document Name field is required.")]
    [StringLength(50)]
    [Display(Name = "Document Name")]
    [Unicode(false)]
    public string Doc_name { get; set; } = null!;

    [Required(ErrorMessage = "The Source Document field is required.")]
    [StringLength(50)]
    [Display(Name = "Source Document")]
    [Unicode(false)]
    public string Source_Document { get; set; } = null!;

    [Required(ErrorMessage = "The Created Date field is required.")]
    [Column(TypeName = "date")]
    [DataType(DataType.Date)]
    [Display(Name = "Created on")]
    public DateTime created_date { get; set; }

    public int? backorder_doc_id { get; set; }

    [Required(ErrorMessage = "The Status field is required.")]
    [StringLength(50)]
    [Display(Name = "Status")]
    [Unicode(false)]
    public string status { get; set; } = null!;

    [Required(ErrorMessage = "The Operation Type field is required.")]
    [StringLength(50)]
    [Display(Name = "Operation Type")]
    [Unicode(false)]
    public string operation_type { get; set; } = null!;

    [InverseProperty("backorder_doc")]
    public virtual ICollection<Transfer> Inversebackorder_doc { get; set; } = new List<Transfer>();

    [InverseProperty("transfer")]
    public virtual ICollection<Transfer_Detail> Transfer_Details { get; set; } = new List<Transfer_Detail>();

    [ForeignKey("backorder_doc_id")]
    [InverseProperty("Inversebackorder_doc")]
    [Display(Name ="Backorder Doc")]
    public virtual Transfer? backorder_doc { get; set; }
}
