using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.Web.DBTables;

public partial class Sale_Order_Detail
{
    [Key]
    public int ID { get; set; }

    public int sale_id { get; set; }

    public int product_id { get; set; }

    public int quantity { get; set; }

    [Column(TypeName = "money")]
    public decimal price { get; set; }

    [ForeignKey("product_id")]
    [InverseProperty("Sale_Order_Details")]
    public virtual Product product { get; set; } = null!;

    [ForeignKey("sale_id")]
    [InverseProperty("Sale_Order_Details")]
    public virtual Sale_Order sale { get; set; } = null!;
}
