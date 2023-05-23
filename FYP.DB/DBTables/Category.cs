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
    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("category_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string CategoryName { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [Column("created_on", TypeName = "date")]
    public DateTime? CreatedOn { get; set; }

    [Column("last_modified", TypeName = "date")]
    public DateTime? LastModified { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
