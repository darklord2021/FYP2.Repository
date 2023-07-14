using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FYP.DB.DBTables;

public partial class Payment
{
    public int id { get; set; }
    [Required]
    [Display(Name = "Method Name")]
    public string method_name { get; set; } = null!;

    public virtual ICollection<Purchase_Order> Purchase_Orders { get; set; } = new List<Purchase_Order>();

    public virtual ICollection<Sale_Order> Sale_Orders { get; set; } = new List<Sale_Order>();
}
