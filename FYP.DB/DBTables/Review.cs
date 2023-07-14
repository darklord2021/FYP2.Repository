using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FYP.DB.DBTables;

public partial class Review
{
    public int id { get; set; }
    [Display(Name = "Stars")]
    public byte? stars { get; set; }
    [Display(Name = "Description")]
    public string Description { get; set; } = null!;
    [Display(Name = "Customer")]
    public int? customer_id { get; set; }
    [Display(Name = "Product")]
    public int? product_id { get; set; }

    public virtual Customer? customer { get; set; }

    public virtual Product? product { get; set; }
}
