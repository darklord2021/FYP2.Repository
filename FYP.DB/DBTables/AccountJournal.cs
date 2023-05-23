using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Table("Account_Journal")]
public partial class AccountJournal
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("account_id")]
    public int AccountId { get; set; }

    [Column("account")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Account { get; set; }

    [Column("debit", TypeName = "money")]
    public decimal? Debit { get; set; }

    [Column("credit", TypeName = "money")]
    public decimal? Credit { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("AccountJournals")]
    public virtual AccountMove AccountNavigation { get; set; } = null!;
}
