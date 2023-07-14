using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FYP.DB.DBTables;

public partial class Transfer_Detail
{
    public int id { get; set; }
    [Required]
    public int? transfer_id { get; set; }
    [Required]
    public int? product_id { get; set; }
    [Required]
    public int? demand { get; set; }

    public int? done { get; set; }

    public virtual Product? product { get; set; }

    public virtual Transfer? transfer { get; set; }
}
