using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

public partial class Vendor
{
    [Key]
    [Column("vendor_id")]
    public int VendorId { get; set; }

    [Column("name")]
    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("NTN")]
    public long? Ntn { get; set; }

    [Column("phone_number")]
    public long? PhoneNumber { get; set; }

    [Column("email_address")]
    [StringLength(100)]
    [Unicode(false)]
    public string? EmailAddress { get; set; }

    [Column("vendor_address", TypeName = "text")]
    public string? VendorAddress { get; set; }

    [Column("website")]
    [StringLength(150)]
    [Unicode(false)]
    public string? Website { get; set; }

    [InverseProperty("Vendor")]
    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
