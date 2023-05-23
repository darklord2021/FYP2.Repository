using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Table("Invoice_lines")]
public partial class InvoiceLine
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("qty")]
    public int Qty { get; set; }

    [Column("price", TypeName = "money")]
    public decimal Price { get; set; }

    [Column("taxes")]
    public double Taxes { get; set; }

    [Column("account_id")]
    public int AccountId { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("InvoiceLines")]
    public virtual AccountMove Account { get; set; } = null!;
}
