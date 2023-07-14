using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.Web.DBTables;

[Table("Category")]
public partial class Category
{
    [Key]
    public int category_id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string category_name { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [Column(TypeName = "date")]
    public DateTime? created_on { get; set; }

    [Column(TypeName = "date")]
    public DateTime? last_modified { get; set; }

    [InverseProperty("category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
