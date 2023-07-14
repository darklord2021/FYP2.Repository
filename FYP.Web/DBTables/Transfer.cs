using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.Web.DBTables;

public partial class Transfer
{
    [Key]
    public int ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Doc_name { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Source_Document { get; set; }

    [Column(TypeName = "date")]
    public DateTime? created_date { get; set; }

    public int? backorder_doc_id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string status { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? operation_type { get; set; }

    [InverseProperty("backorder_doc")]
    public virtual ICollection<Transfer> Inversebackorder_doc { get; set; } = new List<Transfer>();

    [InverseProperty("transfer")]
    public virtual ICollection<Transfer_Detail> Transfer_Details { get; set; } = new List<Transfer_Detail>();

    [ForeignKey("backorder_doc_id")]
    [InverseProperty("Inversebackorder_doc")]
    public virtual Transfer? backorder_doc { get; set; }
}
