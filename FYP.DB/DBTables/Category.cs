using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FYP.DB.DBTables;

public partial class Category
{
    public int category_id { get; set; }
    [Required]
    [Display(Name = "Name")]
    public string category_name { get; set; } = null!;
    [Required]
    [Display(Name = "Description")]
    public string? Description { get; set; }
    [Required]
    [Display(Name = "Created on")]
    public DateTime? created_on { get; set; }
    [Display(Name = "Modified on")]

    public DateTime? last_modified { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
