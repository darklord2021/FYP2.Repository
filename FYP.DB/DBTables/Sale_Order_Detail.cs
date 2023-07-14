using System;
using System.Collections.Generic;

namespace FYP.DB.DBTables;

public partial class Sale_Order_Detail
{
    public int ID { get; set; }

    public int sale_id { get; set; }

    public int product_id { get; set; }

    public int quantity { get; set; }

    public decimal price { get; set; }

    public virtual Product product { get; set; } = null!;

    public virtual Sale_Order sale { get; set; } = null!;
}
