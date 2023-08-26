using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Table("Category")]
public partial class Category
{
    [Key]
    public int category_id { get; set; }

    [Required(ErrorMessage = "The Category Name is required.")]
    [StringLength(50, ErrorMessage = "The Category Name must be at most {1} characters long.")]
    [Display(Name = "Category Name")]
    [Unicode(false)]
    public string category_name { get; set; } = null!;

    [Required(ErrorMessage = "The Description is required.")]
    [Column(TypeName = "text")]
    [Display(Name = "Description")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "The Created On date is required.")]
    [Column(TypeName = "date")]
    [Display(Name = "Created On")]
    [DataType(DataType.Date)]
    public DateTime created_on { get; set; }

    [Column(TypeName = "date")]
    [Display(Name = "Last Modified")]
    [DataType(DataType.Date)]
    public DateTime? last_modified { get; set; }

    [InverseProperty("category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
