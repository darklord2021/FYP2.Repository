using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FYP.DB.DBTables;

public partial class Vendor
{
    public int vendor_id { get; set; }
    [Required]
    [Display(Name = "Name")]
    public string name { get; set; } = null!;
    [MaxLength(13)]
    [Display(Name = "NTN")]
    public long? NTN { get; set; }
    [Required]
    [Range(1000000000, 999999999999999999, ErrorMessage = "Please enter a valid phone number.")]
    [Display(Name = "Phone")]
    public long? phone_number { get; set; }
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address.")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    public string? email_address { get; set; }
    [Required]
    [DataType(DataType.MultilineText)]
    [Display(Name = "Address")]
    public string? vendor_address { get; set; }
    [DataType(DataType.Url)]
    [Display(Name ="Website")]
    public string? website { get; set; }

    public virtual ICollection<Purchase_Order> Purchase_Orders { get; set; } = new List<Purchase_Order>();
}
