using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

public partial class Transfer
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("Doc_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? DocName { get; set; }

    [Column("Source_Document")]
    [StringLength(50)]
    [Unicode(false)]
    public string? SourceDocument { get; set; }

    [Column("created_date", TypeName = "date")]
    public DateTime? CreatedDate { get; set; }

    [Column("backorder_doc_id")]
    public int? BackorderDocId { get; set; }

    [Column("status")]
    [StringLength(50)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    [ForeignKey("BackorderDocId")]
    [InverseProperty("InverseBackorderDoc")]
    public virtual Transfer? BackorderDoc { get; set; }

    [InverseProperty("BackorderDoc")]
    public virtual ICollection<Transfer> InverseBackorderDoc { get; set; } = new List<Transfer>();

    [InverseProperty("Transfer")]
    public virtual ICollection<TransferDetail> TransferDetails { get; set; } = new List<TransferDetail>();
}
