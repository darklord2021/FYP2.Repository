using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Table("Shipping_Address")]
public partial class Shipping_Address
{
    [Key]
    public int id { get; set; }

    [Required(ErrorMessage = "The Shipping Address field is required.")]
    [Column("Shipping_Address", TypeName = "text")]
    [Display(Name = "Shipping Address")]
    [DataType(DataType.MultilineText)] // Optional: If you want to specify it as multiline text
    public string Shipping_Address1 { get; set; } = null!;

    [Required(ErrorMessage = "The Customer ID field is required.")]
    public int customer_id { get; set; }

    [ForeignKey("customer_id")]
    [InverseProperty("Shipping_Addresses")]
    public virtual Customer customer { get; set; } = null!;
}
