using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Table("User_Auth")]
public partial class User_Auth
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "The Username field is required.")]
    [StringLength(50)]
    [Display(Name = "Username")]
    [Unicode(false)]
    public string username { get; set; } = null!;

    [Required(ErrorMessage = "The Password field is required.")]
    [StringLength(50)]
    [Display(Name = "Password")]
    [Unicode(false)]
    [DataType(DataType.Password)]
    public string password { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessage = "Invalid role value. Role must be a positive integer.")]
    public int role { get; set; }
}
