using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

public partial class Customer
{
    [Key]
    public int customer_id { get; set; }

    [Required(ErrorMessage = "The Customer Name field is required.")]
    [StringLength(50)]
    [Display(Name = "Customer Name")]
    [Unicode(false)]
    public string customer_name { get; set; } = null!;

    [Required(ErrorMessage = "The Email field is required.")]
    [StringLength(100)]
    //[RegularExpression(@"/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/", ErrorMessage = "Invalid email address.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [Display(Name = "Email Address")]
    [Unicode(false)]
    public string email { get; set; } = null!;

    [Required(ErrorMessage = "The Phone field is required.")]
    [RegularExpression(@"^\d{11,13}$", ErrorMessage = "Invalid phone number. It must be 11 to 13 digits.")]
    [Display(Name = "Phone Number")]
    public long phone { get; set; }

    [Required(ErrorMessage = "The Address field is required.")]
    [Column(TypeName = "text")]
    [DataType(DataType.MultilineText)]
    [Display(Name = "Address")]
    public string address { get; set; } = null!;

    [Range(0, double.MaxValue, ErrorMessage = "Record must be a non-negative number.")]
    [Display(Name = "Record")]
    public double? record { get; set; }

    [InverseProperty("customer")]
    public virtual ICollection<Billing_Address> Billing_Addresses { get; set; } = new List<Billing_Address>();

    [InverseProperty("customer")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("customer")]
    public virtual ICollection<Sale_Order> Sale_Orders { get; set; } = new List<Sale_Order>();

    [InverseProperty("customer")]
    public virtual ICollection<Shipping_Address> Shipping_Addresses { get; set; } = new List<Shipping_Address>();
}
