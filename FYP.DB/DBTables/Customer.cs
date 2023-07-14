using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FYP.DB.DBTables;

public partial class Customer
{
    public int customer_id { get; set; }
    [Required]
    [Display(Name = "Name")]
    public string? customer_name { get; set; }
    [Required]
    [Display(Name = "Email")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address.")]
    [DataType(DataType.EmailAddress)]
    public string? email { get; set; }
    [Required]
    [Display(Name = "Phone")]
    [Range(1000000000, 999999999999999999, ErrorMessage = "Please enter a valid phone number.")]
    public long? phone { get; set; }
    [Required]
    [Display(Name = "Address")]
    [DataType(DataType.MultilineText)]
    public string? address { get; set; }
    [Display(Name = "Rating")]
    public double? record { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Sale_Order> Sale_Orders { get; set; } = new List<Sale_Order>();
}
