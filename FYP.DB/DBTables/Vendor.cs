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

    [StringLength(100)]
    [Unicode(false)]
    public string name { get; set; } = null!;

    public long? NTN { get; set; }

    public long? phone_number { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? email_address { get; set; }

    [Column(TypeName = "text")]
    public string? vendor_address { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? website { get; set; }

    [InverseProperty("vendor")]
    public virtual ICollection<Purchase_Order> Purchase_Orders { get; set; } = new List<Purchase_Order>();
}
