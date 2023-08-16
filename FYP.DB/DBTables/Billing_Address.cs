using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Table("Billing_Address")]
public partial class Billing_Address
{
    [Key]
    public int id { get; set; }

    [Required(ErrorMessage = "The Billing Address is required.")]
    [Column("Billing_Address", TypeName = "text")]
    [Display(Name = "Billing Address")]
    [DataType(DataType.MultilineText)]
    public string Billing_Address1 { get; set; } = null!;

    [Required(ErrorMessage = "The Customer ID field is required.")]
    public int customer_id { get; set; }

    [ForeignKey("customer_id")]
    [InverseProperty("Billing_Addresses")]
    public virtual Customer customer { get; set; } = null!;
}
