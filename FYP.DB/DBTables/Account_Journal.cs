using System;
using System.Collections.Generic;

namespace FYP.DB.DBTables;

public partial class Account_Journal
{
    public int ID { get; set; }

    public int account_id { get; set; }

    public string? account { get; set; }

    public decimal? debit { get; set; }

    public decimal? credit { get; set; }
}
