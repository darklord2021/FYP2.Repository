using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

public partial class Invoice_line
{
    [Key]
    public int ID { get; set; }

    public int product_id { get; set; }

    public int qty { get; set; }

    [Column(TypeName = "money")]
    public decimal price { get; set; }

    public double taxes { get; set; }

    public int account_id { get; set; }

    [ForeignKey("account_id")]
    [InverseProperty("Invoice_lines")]
    public virtual Account_Move account { get; set; } = null!;
}
