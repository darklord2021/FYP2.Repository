using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

public partial class Vendor
{
    [Key]
    public int vendor_id { get; set; }

    [Required(ErrorMessage = "The Name field is required.")]
    [StringLength(100)]
    [Display(Name = "Name")]
    [Unicode(false)]
    public string name { get; set; } = null!;

    [Display(Name = "NTN")]
    [RegularExpression(@"^\d{8}$", ErrorMessage = "NTN must be 8 digits.")]
    [DataType(DataType.Text)] // DataType.Text represents a non-multiline text input
    public long? NTN { get; set; }

    [Required(ErrorMessage = "The Phone Number field is required.")]
    [Display(Name = "Phone Number")]
    //[RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number format. The phone number should start with '0' and have 10 digits.")]
    //[DataType(DataType.PhoneNumber)]
    public long phone_number { get; set; } // Changed to string type

    [Required(ErrorMessage = "The Email Address field is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    [StringLength(100)]
    [Display(Name = "Email Address")]
    [Unicode(false)]
    [DataType(DataType.EmailAddress)]
    public string email_address { get; set; } = null!;

    [Required(ErrorMessage = "The Vendor Address field is required.")]
    [Column(TypeName = "text")]
    [Display(Name = "Vendor Address")]
    [DataType(DataType.MultilineText)]
    public string vendor_address { get; set; } = null!;

    [StringLength(150)]
    [Display(Name = "Website")]
    [Url(ErrorMessage = "Invalid website URL format.")]
    [Unicode(false)]
    [DataType(DataType.Url)]
    public string? website { get; set; }

    [InverseProperty("vendor")]
    public virtual ICollection<Purchase_Order> Purchase_Orders { get; set; } = new List<Purchase_Order>();
}
