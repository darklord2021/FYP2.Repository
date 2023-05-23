using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Table("Account_Journal")]
public partial class Account_Journal
{
    [Key]
    public int ID { get; set; }

    public int account_id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? account { get; set; }

    [Column(TypeName = "money")]
    public decimal? debit { get; set; }

    [Column(TypeName = "money")]
    public decimal? credit { get; set; }

    [ForeignKey("account_id")]
    [InverseProperty("Account_Journals")]
    public virtual Account_Move accountNavigation { get; set; } = null!;
}
