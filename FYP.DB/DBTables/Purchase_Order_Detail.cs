using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

public partial class Purchase_Order_Detail
{
    [Key]
    public int ID { get; set; }

    public int? purchase_id { get; set; }

    public int? product_id { get; set; }

    public int? quantity { get; set; }

    [Column(TypeName = "money")]
    public decimal? price { get; set; }

    [ForeignKey("product_id")]
    [InverseProperty("Purchase_Order_Details")]
    public virtual Product? product { get; set; }

    [ForeignKey("purchase_id")]
    [InverseProperty("Purchase_Order_Details")]
    public virtual Purchase_Order? purchase { get; set; }
}
