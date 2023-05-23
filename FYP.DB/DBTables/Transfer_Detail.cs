using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

public partial class Transfer_Detail
{
    [Key]
    public int id { get; set; }

    public int? transfer_id { get; set; }

    public int? product_id { get; set; }

    public int? demand { get; set; }

    public int? done { get; set; }

    [ForeignKey("product_id")]
    [InverseProperty("Transfer_Details")]
    public virtual Product? product { get; set; }

    [ForeignKey("transfer_id")]
    [InverseProperty("Transfer_Details")]
    public virtual Transfer? transfer { get; set; }
}
