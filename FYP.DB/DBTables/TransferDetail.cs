using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Table("Transfer_Details")]
public partial class TransferDetail
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("transfer_id")]
    public int? TransferId { get; set; }

    [Column("product_id")]
    public int? ProductId { get; set; }

    [Column("demand")]
    public int? Demand { get; set; }

    [Column("done")]
    public int? Done { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("TransferDetails")]
    public virtual Product? Product { get; set; }

    [ForeignKey("TransferId")]
    [InverseProperty("TransferDetails")]
    public virtual Transfer? Transfer { get; set; }
}
