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

    [Required(ErrorMessage = "The Account Name is required.")]
    [StringLength(50, ErrorMessage = "The Account Name must be at most {1} characters long.")]
    [Display(Name = "Account Name")]
    public string? account { get; set; }

    [Required(ErrorMessage = "The Account ID is required.")]
    public int account_id { get; set; }

    [Column(TypeName = "money")]
    [Display(Name = "Debit Amount")]
    [Range(0, double.MaxValue, ErrorMessage = "The debit amount must be a non-negative value.")]
	[DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
	public decimal? debit { get; set; }

    [Column(TypeName = "money")]
    [Display(Name = "Credit Amount")]
    [Range(0, double.MaxValue, ErrorMessage = "The credit amount must be a non-negative value.")]
	[DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
	public decimal? credit { get; set; }
}
