using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Table("Payment")]
public partial class Payment
{
    [Key]
    public int id { get; set; }

    [Required(ErrorMessage = "The Method Name field is required.")]
    [StringLength(50)]
    [Display(Name = "Method Name")]
    [Unicode(false)]
    public string method_name { get; set; } = null!;

    [InverseProperty("payment_methodNavigation")]
    public virtual ICollection<Purchase_Order> Purchase_Orders { get; set; } = new List<Purchase_Order>();

    [InverseProperty("payment_methodNavigation")]
    public virtual ICollection<Sale_Order> Sale_Orders { get; set; } = new List<Sale_Order>();
}
